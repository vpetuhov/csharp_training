using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;
        private List<string> contactDetails;

        public ContactData()
        {
        }

        public ContactData(string firstName, string lastname)
        {
            FirstName = firstName;
            LastName = lastname;
        }

        [Column(Name = "firstname"), NotNull]
        public string FirstName { get; set; }

        [Column(Name = "middlename"), NotNull]
        public string MiddleName { get; set; }

        [Column(Name = "lastname"), NotNull]
        public string LastName { get; set; }

        [Column(Name = "nickname"), NotNull]
        public string NickName { get; set; }

        [Column(Name = "company"), NotNull]
        public string Company { get; set; }

        [Column(Name = "title"), NotNull]
        public string Title { get; set; }

        [Column(Name = "address"), NotNull]
        public string Address { get; set; }

        [Column(Name = "home"), NotNull]
        public string HomePhone { get; set; }

        [Column(Name = "mobile"), NotNull]
        public string MobilePhone { get; set; }

        [Column(Name = "work"), NotNull]
        public string WorkPhone { get; set; }

        [Column(Name = "fax"), NotNull]
        public string Fax { get; set; }

        [Column(Name = "email"), NotNull]
        public string Email { get; set; }

        [Column(Name = "email2"), NotNull]
        public string Email2 { get; set; }

        [Column(Name = "email3"), NotNull]
        public string Email3 { get; set; }

        [Column(Name = "homepage"), NotNull]
        public string HomePage { get; set; }

        [Column(Name = "address2"), NotNull]
        public string SecondAddress { get; set; }

        [Column(Name = "phone2"), NotNull]
        public string SecondPhone { get; set; }

        [Column(Name = "notes"), NotNull]
        public string Notes { get; set; }

        [Column(Name = "id"), PrimaryKey, Identity]
        public string Id { get; set; }

        [Column(Name = "deprecated"), NotNull]
        public string Deprecated { get; set; }

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
            return "LastName:" + LastName + "\nMiddleName:" + MiddleName + "\nFirstName:" + FirstName
                + "\nNickName:" + NickName + "\nCompany:" + Company + "\nTitle:" + Title
                + "\nAdress:" + Address + "\nHomePhone" + HomePhone + "\nWorkPhone" + WorkPhone
                + "\nMobilePhone" + MobilePhone + "\nFax" + Fax + "\nEmail" + Email
                + "\nEmail2" + Email2 + "\nEmail3" + Email3 + "\nHomePage" + HomePage
                + "\nSecondAddress" + SecondAddress + "\nSecondPhone" + SecondPhone + "\nNotes" + Notes;
        }

        public static List<ContactData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts.Where(x => x.Deprecated == "0000-00-00 00:00:00") select c).ToList();
            }
        }
    }
}