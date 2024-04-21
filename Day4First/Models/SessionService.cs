namespace Day4First.Models
{
    public class SessionService
    {
        private readonly IHttpContextAccessor _sessionContxt;
        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _sessionContxt = httpContextAccessor;
        }
        public void SetAuthorSessionValue(AuthorCheckResult AuthorSessionValues)
        {
            _sessionContxt.HttpContext.Session.SetInt32("AuthorId",AuthorSessionValues.AuthorFound.Id);
            _sessionContxt.HttpContext.Session.SetString("AuthorName", AuthorSessionValues.AuthorFound.Username);
            _sessionContxt.HttpContext.Session.SetString("AuthorEmail", AuthorSessionValues.AuthorFound.Email);
            _sessionContxt.HttpContext.Session.SetString("IsLoggedIn", "true");
            _sessionContxt.HttpContext.Session.SetString("IsAuthor", "true");
        }
        public void SetUserSessionValue(UserCheckResult UserSessionValues)
        {
            _sessionContxt.HttpContext.Session.SetInt32("UserId",UserSessionValues.UserFound.Id);
            _sessionContxt.HttpContext.Session.SetString("UserName", UserSessionValues.UserFound.Username);
            _sessionContxt.HttpContext.Session.SetString("UserEmail", UserSessionValues.UserFound.Email);
            _sessionContxt.HttpContext.Session.SetString("IsLoggedIn", "true");
            _sessionContxt.HttpContext.Session.SetString("IsAuthor", "false");
        }
    }
}
