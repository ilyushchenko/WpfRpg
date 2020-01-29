using System;
using Core.Data;

namespace UI
{
    public class CAuthController
    {
        private static readonly Lazy<CAuthController> _lazyAuthController =
            new Lazy<CAuthController>(() => new CAuthController());

        public static CAuthController Instance => _lazyAuthController.Value;

        public String Session { get; private set; }
        public Guid PlayerId { get; private set; }

        public String Login { get; private set; }

        public void SetUser(CPlayer player)
        {
            Login = player.Login;
            PlayerId = player.Id;
            Session = player.Session;
        }
    }
}