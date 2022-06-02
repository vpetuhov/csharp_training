using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;
        private List<string> contactDetails;
        public ContactData(string firstName, string lastname)
        {
            FirstName = firstName;
            LastName = lastname;
        }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public string HomePage { get; set; }
        public string SecondAddress { get; set; }
        public string SecondPhone { get; set; }
        public string Notes { get; set; }
        public string Id { get; set; }

        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUpPhones(HomePhone) + CleanUpPhones(MobilePhone) + CleanUpPhones(WorkPhone)) + CleanUpPhones(SecondPhone).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }
        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return (CleanUpEmails(Email) + CleanUpEmails(Email2) + CleanUpEmails(Email3)).Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }
        private string CleanUpPhones(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            else
            {
                return Regex.Replace(phone, "[- ()]", "") + "\r\n";
            }
        }
        private string CleanUpEmails(string email)
        {
            if (email == null || email == "")
            {
                return "";
            }

            return email + "\r\n";
        }

        public List<string> ContactDetails
        {
            get
            {
                if (contactDetails != null)
                {
                    return contactDetails;
                }
                else
                {
                    List<string> result = new List<string>();
                    StringBuilder str = new StringBuilder();
                    AddContactDetailsToOneString(str, FirstName);
                    AddContactDetailsToOneString(str, MiddleName);
                    AddContactDetailsToOneString(str, LastName);
                    CleanUpFioStringAndAddToResult(str, result);

                    AddContactDetailsToResult(result, NickName);

                    AddContactDetailsToResult(result, Title);
                    AddContactDetailsToResult(result, Company);
                    AddContactDetailsToResult(result, Address);

                    AddContactDetailsToResult(result, HomePhone);
                    AddContactDetailsToResult(result, MobilePhone);
                    AddContactDetailsToResult(result, WorkPhone);
                    AddContactDetailsToResult(result, Fax);

                    AddContactDetailsToResult(result, Email);
                    AddContactDetailsToResult(result, Email2);
                    AddContactDetailsToResult(result, Email3);
                    AddContactDetailsToResult(result, HomePage);

                    AddContactDetailsToResult(result, SecondAddress);
                    AddContactDetailsToResult(result, SecondPhone);
                    AddContactDetailsToResult(result, Notes);

                    if (result.Count == 0)
                    {
                        result.Add("");
                    }

                    return result;
                }
            }
            set
            {
                contactDetails = value;
            }
        }
        private void AddContactDetailsToOneString(StringBuilder str, string property)
        {
            if (property == FirstName || property == LastName || property == MiddleName)
            {
                if (!string.IsNullOrEmpty(property))
                {
                    str.Append(property + " ");
                }
            }
        }
        private void AddContactDetailsToResult(List<string> result, string property)
        {
            if (!string.IsNullOrEmpty(property))
            {
                if (property == HomePhone)
                    result.Add("H: " + property);
                else if (property == MobilePhone)
                    result.Add("M: " + property);
                else if (property == WorkPhone)
                    result.Add("W: " + property);
                else if (property == Fax)
                    result.Add("F: " + property);
                else if (property == SecondPhone)
                    result.Add("P: " + property);
                else if (property == Email || property == Email2 || property == Email3)
                    result.Add(property.Trim());
                else if (property == HomePage)
                {
                    result.Add("Homepage:");
                    result.Add(property.Replace("http://", ""));
                }
                else
                    result.Add(property);
            }
        }

        private void CleanUpFioStringAndAddToResult(StringBuilder str, List<string> result)
        {
            if (str.Length > 1)
            {
                result.Add(str.ToString().Trim());
            }
            str.Clear();
        }

        public bool Equals(ContactData other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (object.ReferenceEquals(this, other))
            {
                return true;
            }

            return LastName == other.LastName && FirstName == other.FirstName;
        }

        public override int GetHashCode()
        {
            return (LastName + FirstName).GetHashCode();
        }
        public int CompareTo(ContactData other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return 1;
            }

            if (LastName.CompareTo(other.LastName) != 0)
            {
                return LastName.CompareTo(other.LastName);
            }
            else if (FirstName.CompareTo(other.FirstName) != 0)
            {
                return FirstName.CompareTo(other.FirstName);
            }

            return 0;
        }
        public override string ToString()
        {
            return string.Format("LastName: {0}, FirstName: {1}", LastName, FirstName);
        }
    }
}