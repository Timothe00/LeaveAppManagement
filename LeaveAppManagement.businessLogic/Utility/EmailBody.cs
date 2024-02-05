

namespace LeaveAppManagement.businessLogic.Utility
{
    public class EmailBody
    {
        public static string EmailStringBody(string email, string emailToken)
        {
            return 
            $@"
            <!DOCTYPE html>
            <html>

            <head>
                <title>Réinitialisez votre mot de passe</title>
            </head>

            <body style="" margin:0; padding:0; font-family: Arial, Helvetica, sans-serif;"">
                <div style="" height: auto; background: linear-gradient(to top, #c9c9ff 50%, #6e6ef6 98%) no-repeat; width:
                    400px;padding: 30px"">
                    <div>
                        <h1>Réinitialisez votre mot de passe</h1>
                        <hr>
                        <p>Vous recevez cet e-mail car vous avez demandé une réinitialisation du mot de passe pour votre compte
                            LeaveAppManagement.</p>
                        <p>Veuillez appuyer sur le bouton ci-dessous pour choisir un nouveau mot de passe.</p>
                        <a href="" http://localhost:4200/auth/reset?email={email}&code={emailToken}"" target="" _blank"" style=""
                            background:#0d6efc;color:white;border-radius: 4px;display:block;margin:0 auto;width:
                            50%;text-align:center;text-decoration:none"">réinitialiser le mot de passe</a>
                        <p>Cordialement, <br><br>
                            InFi SoFtWare</p>
                    </div>
                </div>
            </body>

            </html>
            ";
        }
    }
}
