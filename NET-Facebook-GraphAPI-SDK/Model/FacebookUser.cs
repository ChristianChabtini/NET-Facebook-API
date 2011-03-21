
namespace NetFacebookGraphApiSDK.Model
{

   public class FacebookUser 
    {
        private long _id;
        private string _first_name;
        private string _last_name;
        private string _name;
        private string _birthday;

        public long id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string first_name
        {
            get { return _first_name; }
            set { _first_name = value; }
        }

        public string last_name
        {
            get { return _last_name; }
            set { _last_name = value; }
        }

        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
        }
    }
}