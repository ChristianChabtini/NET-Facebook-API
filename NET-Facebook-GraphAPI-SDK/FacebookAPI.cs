using System.IO;
using System.Net;
using System.Collections.Specialized;
using System.Runtime.Serialization.Json;
using System.Web;
using NetFacebookGraphApiSDK.Model;

namespace NetFacebookGraphApiSDK
{
    public class FacebookAPI
    {
        private const string FRIENDS = "friends";
        private const string NEWS_FEED = "home";
        private const string WALL = "feed";
        private const string LIKES = "likes";
        private const string MOVIES = "movies";
        private const string MUSIC = "music";
        private const string BOOKS = "books";
        private const string NOTES = "notes";
        private const string PHOTO_TAGS = "photos";
        private const string PHOTO_ALBUMS = "albums";
        private const string VIDEO_TAGS = "videos";
        private const string VIDEO_UPLOADS = "videos/uploaded";
        private const string EVENTS = "events";
        private const string GROUPS = "groups";
        private const string CHECK_INS = "checkins";
        private const string PROFILE_PICTURE = "picture";

        private const string BASE_AUTHENTICATION_URL = "https://www.facebook.com/dialog/oauth";
        private const string ACCESS_TOKEN_URL = "https://graph.facebook.com/oauth/access_token";
        private const string GRAPH_GET_URL = "https://graph.facebook.com/me/{0}?access_token={1}";

        private string appId = string.Empty;
        private string appSecret = string.Empty;
        private string accessToken = string.Empty;
        private string userAgent = string.Empty;

        #region Properties

        public string AppId
        {
            get { return appId; }
            set { appId = value; }
        }

        public string AppSecret
        {
            get { return appSecret; }
            set { appSecret = value; }
        }

        public string AccessToken
        {
            get { return accessToken; }
            set { accessToken = value; }
        }

        public string UserAgent
        {
            get { return userAgent; }
            set { userAgent = value; }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_appId"></param>
        /// <param name="_appSecret"></param>
        public FacebookAPI(string _appId, string _appSecret)
        {
            this.appId = _appId;
            this.appSecret = _appSecret;
        }


        public void Authorize(HttpResponseBase response, string callbackUrl)
        {
            string _target = string.Format("{0}?client_id={1}&redirect_uri={2}",
                BASE_AUTHENTICATION_URL, AppId, callbackUrl);
            response.Redirect(_target);
        }

        public bool RetrieveAccessToken(string code, string callbackUrl)
        {
            string _target = string.Format("{0}?client_id={1}&redirect_uri={2}&client_secret={3}&code={4}",
                ACCESS_TOKEN_URL, AppId, callbackUrl, AppSecret, code);

            using (WebClient client = new WebClient())
            {
                client.Headers["User-Agent"] = UserAgent;
                using (StreamReader rdr = new StreamReader(client.OpenRead(_target)))
                {
                    NameValueCollection qs;
                    qs = HttpUtility.ParseQueryString(rdr.ReadToEnd());
                    if (qs["access_token"].Length > 0)
                    {
                        this.AccessToken = qs["access_token"];
                        return true;
                    }
                    return false;
                }
            }
        }

        public string GetFriends()
        {
            return GetGraphDataReq(FRIENDS);
        }

        public string GetNewsFeed()
        {
            return GetGraphDataReq(NEWS_FEED);
        }

        public string GetWall()
        {
            return GetGraphDataReq(WALL);
        }

        public string GetLikes()
        {
            return GetGraphDataReq(LIKES);
        }

        public string GetMovies()
        {
            return GetGraphDataReq(MOVIES);
        }

        public string GetMusic()
        {
            return GetGraphDataReq(MUSIC);
        }

        public string GetBooks()
        {
            return GetGraphDataReq(BOOKS);
        }

        public string GetNotes()
        {
            return GetGraphDataReq(NOTES);
        }

        public string GetPhotoTags()
        {
            return GetGraphDataReq(PHOTO_TAGS);
        }

        public string GetPhotoAlbums()
        {
            return GetGraphDataReq(PHOTO_ALBUMS);
        }

        public string GetVideoTags()
        {
            return GetGraphDataReq(VIDEO_TAGS);
        }

        public string GetVideoUploads()
        {
            return GetGraphDataReq(VIDEO_UPLOADS);
        }

        public string GetEvents()
        {
            return GetGraphDataReq(EVENTS);
        }

        public string GetGroups()
        {
            return GetGraphDataReq(GROUPS);
        }

        public string GetCheckins()
        {
            return GetGraphDataReq(CHECK_INS);
        }

        public FacebookUser GetUser()
        {
            var _target = string.Format(GRAPH_GET_URL, "", AccessToken);
            FacebookUser user = new FacebookUser();

            using (WebClient client = new WebClient())
            {
                client.Headers["User-Agent"] = UserAgent;

                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(FacebookUser));
                user = serializer.ReadObject(client.OpenRead(_target)) as FacebookUser;
            }
            return user;
        }

        private string GetGraphDataReq(string graphData)
        {
            var _target = string.Format(GRAPH_GET_URL, graphData, AccessToken);
            using (WebClient client = new WebClient())
            {
                client.Headers["User-Agent"] = UserAgent;
                using (StreamReader rdr = new StreamReader(client.OpenRead(_target)))
                {
                    return rdr.ReadToEnd();
                }
            }
        }
    }

    
}