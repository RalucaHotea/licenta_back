﻿using BusinessObjectLayer;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;

namespace BoschStore
{
    public static class LdapWrapper
    {
        private static DirectoryEntry SearchRoot =
            new DirectoryEntry("GC://DC=bosch,DC=com")
            {
                AuthenticationType = AuthenticationTypes.Secure,
                Username = Constants.NTUser,
                Password = Constants.NTPassword
            };

        public static string GetUserNameByEmailAddress(string email)
            => GetUserProperty(email, "mail", "SAMAccountName");

        public static string GetDisplayNameByUserName(string userName)
            => GetUserProperty(userName, "SAMAccountName", "DisplayName");

        public static string GetOfficeStreetAddressByUserName(string officeStreetAddress)
            => GetUserProperty(officeStreetAddress, "SAMAccountName", "streetAddress");

        public static string GetOfficeCountryByUserName(string officeCountryAdress)
            => GetUserProperty(officeCountryAdress, "SAMAccountName", "st");

        public static string GetOfficeCityByUserName(string officeCityAddress)
            => GetUserProperty(officeCityAddress, "SAMAccountName", "l");

        public static string GetUserNameByDisplayName(string displayName)
            => GetUserProperty(displayName, "DisplayName", "SAMAccountName");

        public static string GetEmailAddressByUserName(string userName)
            => GetUserProperty(userName, "SAMAccountName", "mail");

        public static string GetDepartmentByUserName(string userName)
            => GetUserProperty(userName, "SAMAccountName", "department");
        public static string GetGroupByUserName(string userName)
           => GetUserProperty(userName, "SAMAccountName", "group");

        public static string GetCountryByUserName(string userName)
            => GetUserProperty(userName, "SAMAccountName", "co");

        public static string GetFirstNameByUsername(string userName)
            => GetUserProperty(userName, "SAMAccountName", "givenName");

        public static string GetLastNameByUsername(string userName)
            => GetUserProperty(userName, "SAMAccountName", "sn");
        public static ICollection<string> GetGroupUserAcountNames(string department)
            => GetUserPropertyForAllUsersInGroupOrDepartment(department, "SAMAccountName");

        public static ICollection<string> GetDepartmentUserDisplayNames(string department)
            => GetUserPropertyForAllUsersInGroupOrDepartment(department, "displayName", true);

        /// <summary>
        /// Retrieve all structure units where the user is member.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns> A collection of structure units. </returns>
        public static ICollection<string> GetMembershipGroups(string userName)
        {
            var groups = GetMembership(userName).ToList();
            var allGroups = new List<string>(groups);
            var newGroups = new List<string>();

            do
            {
                foreach (var topGroup in groups)
                {
                    newGroups.AddRange(GetMembership(topGroup));
                }

                newGroups = newGroups.Distinct().ToList();
                groups.Clear();

                foreach (var group in newGroups)
                {
                    if (!allGroups.Contains(group, StringComparer.OrdinalIgnoreCase))
                    {
                        allGroups.Add(group);
                        groups.Add(group);
                    }
                }

                newGroups.Clear();
            } while (groups.Count > 0);

            return allGroups.Any() ? allGroups.Distinct().ToList() : null;
        }


        public static string GetDepartmentHeadUserName(string department)
        {
            department = department.Replace("/", "_");
            var depHeadProperty = GetUserProperty(department, "Name", "Member");

            if (!string.IsNullOrEmpty(depHeadProperty))
            {
                var depHeadCN = depHeadProperty
                    .Split(',')
                    .First(c => c.Contains("CN="))
                    .ToString();

                if (!string.IsNullOrEmpty(depHeadCN))
                {
                    return depHeadCN.Split('=')[1];
                }
            }

            return null;
        }

        /// <summary>
        ///     Retrieve a user property for all members of a group or department.
        /// </summary>
        /// <param name="structureUnitTitle"></param>
        /// <param name="searchProperty"></param>
        /// <param name="forDepartment">Set this param to true in case of department.</param>
        /// <returns> The list of required user property. </returns>
        private static ICollection<string> GetUserPropertyForAllUsersInGroupOrDepartment(string structureUnitTitle, string searchProperty, bool forDepartment = false)
        {
            if (structureUnitTitle == null)
            {
                return null;
            }

            var propertiesToLoad = new string[] { searchProperty, "department" };
            var filter = $"(&(objectClass=user)(department={structureUnitTitle}{(forDepartment ? "*" : string.Empty)}))";
            var searchResults = FindAllByFilterAndPropertiesToLoad(filter, propertiesToLoad);

            if (searchResults != null && searchResults.Count > 0)
            {
                var users = new List<string>();

                foreach (SearchResult result in searchResults)
                {
                    if ((result.Properties[searchProperty]?.Count ?? 0) > 0)
                    {
                        users.Add(result.Properties[searchProperty][0].ToString());
                    }
                }

                return users;
            }

            return null;
        }

        /// <summary>
        ///     Find membership based on a username or group name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns> The membership list. </returns>
        private static ICollection<string> GetMembership(string name)
        {
            var propertiesToLoad = new string[] { "Name", "SAMAccountName", "memberOf" };
            var filter = $"(Name={name})";
            var results = new List<string>();
            SearchResult searchResult = FindOneByFilterAndPropertiesToLoad(filter, propertiesToLoad);

            if (searchResult != null)
            {
                foreach (string result in searchResult.Properties["memberOf"])
                {
                    var lineArray = result.Split(',');

                    if (lineArray[0].StartsWith("CN"))
                    {
                        foreach (var element in lineArray)
                        {
                            var elementArray = element.Split('=');

                            foreach (var subelement in elementArray)
                            {
                                if (subelement.Equals("distributionlists", StringComparison.OrdinalIgnoreCase))
                                {
                                    results.Add(lineArray[0].Trim().Substring(3));
                                }
                            }
                        }
                    }
                }
            }

            return results;
        }

        private static string GetDepartmentPrefix(string departmentTitle)
            => departmentTitle.Contains("/") ? departmentTitle.Split('/').First() : string.Empty;

        /// <summary>
        ///     Get property by filter from LDAP
        /// </summary>
        /// <param name="searchFilter"></param>
        /// <param name="searchProperty"></param>
        /// <param name="resultProperty"></param>
        /// <returns>The property content if found otherwise null</returns>
        private static string GetUserProperty(string searchFilter, string searchProperty, string resultProperty)
        {
            var filter = $"({searchProperty}={searchFilter})";
            var propertiesToLoad = new string[] { resultProperty };
            var userProperties = FindOneByFilterAndPropertiesToLoad(filter, propertiesToLoad)?.Properties[resultProperty];

            return (userProperties?.Count ?? 0) > 0
                ? userProperties[0]?.ToString()
                : null;
        }

        private static SearchResult FindOneByFilterAndPropertiesToLoad(string filter, string[] propertiesToLoad)
        {
            var search = new DirectorySearcher(SearchRoot, filter, propertiesToLoad);

            try
            {
                return search.FindOne();
            }
            catch
            {
                return null;
            }
        }

        private static SearchResultCollection FindAllByFilterAndPropertiesToLoad(string filter, string[] propertiesToLoad)
        {
            var search = new DirectorySearcher(SearchRoot, filter, propertiesToLoad);

            try
            {
                return search.FindAll();
            }
            catch
            {
                return null;
            }
        }
    }
}
