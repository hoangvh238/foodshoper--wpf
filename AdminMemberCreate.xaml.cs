using BusinessObject.Model;
using DataAccess.Repository;
using System.Text.RegularExpressions;
using System.Windows;


namespace SaleWPFApp
{
    /// <summary>
    /// Interaction logic for AdminMemberCreate.xaml
    /// </summary>
    public partial class AdminMemberCreate : Window
    {
        private readonly IMemberRepository memberRepository;
        private readonly AdminMember adminMember;
        private Member? member;
        public AdminMemberCreate(AdminMember _adminMember, Member? _member, IMemberRepository _memberRepository)
        { 
            InitializeComponent();
            this.memberRepository = _memberRepository;
            this.adminMember = _adminMember;
            this.member = _member;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (member != null)
            {
                txtBoxEmail.Text = member.Email;
                txtBoxCompanyName.Text = member.CompanyName;
                txtBoxCity.Text = member.City;
                txtBoxCountry.Text = member.Country;
                txtBoxPassword.Text = member.Password;
                txtBoxId.Text = member.MemberId.ToString();
                txtBoxId.Visibility = Visibility.Visible;
                labelId.Visibility = Visibility.Visible;
                btn.Content = "Update";
                this.Height = 550;
            }
        }

        private bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            var hasUpperCase = new Regex(@"[A-Z]+");
            var hasLowerCase = new Regex(@"[a-z]+");
            var hasDigits = new Regex(@"[0-9]+");

            return password.Length >= 8 && hasUpperCase.IsMatch(password) && hasLowerCase.IsMatch(password) && hasDigits.IsMatch(password);
        }

        private bool ValidatedData()
        {
            string email = txtBoxEmail.Text;
            string companyName = txtBoxCompanyName.Text;
            string city = txtBoxCity.Text;
            string country = txtBoxCountry.Text;
            string password = txtBoxPassword.Text;

            if (!ValidateEmail(email))
            {
                MessageBox.Show("Invalid email format. Please enter a valid email address.", "Validation Error");
                return false;
            }

            if (string.IsNullOrWhiteSpace(companyName))
            {
                MessageBox.Show("Company name cannot be empty.", "Validation Error");
                return false;
            }

            if (string.IsNullOrWhiteSpace(city))
            {
                MessageBox.Show("City cannot be empty.", "Validation Error");
                return false;
            }

            if (string.IsNullOrWhiteSpace(country))
            {
                MessageBox.Show("Country cannot be empty.", "Validation Error");
                return false;
            }

            if (!ValidatePassword(password))
            {
                MessageBox.Show("Invalid password format. Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one number.", "Validation Error");
                return false;
            }
            return true;
            MessageBox.Show("All inputs are valid!", "Validation Success");
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
          
            if (!ValidatedData())
            {
                MessageBox.Show("Please re-input");
                return;
            }

            string email = txtBoxEmail.Text;
            string companyName = txtBoxCompanyName.Text;
            string city = txtBoxCity.Text;
            string country = txtBoxCountry.Text;
            string password = txtBoxPassword.Text;


            Member? p = null;
            if (member != null)
            {
                p = member;
            }
            else
            {
                p = new Member();
            }
            p.Email = email;
            p.CompanyName = companyName;
            p.City = city;
            p.Country = country;
            p.Password = password;
            if (member != null)
            {
                memberRepository.Update(p);
            }
            else
            {
                memberRepository.Add(p);
            }
            this.Close();
            adminMember.RefreshListView();
        }
    }
}
