namespace SignSafe.Domain.EmailTemplates
{
    public static class EmailTemplates
    {
        public const string OTP = @"
            <!DOCTYPE html>
            <html xmlns=""http://www.w3.org/1999/xhtml"">
            
            <head>
              <title>Email Verify</title>
              <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
              <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"">
              <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
              <link href=""https://fonts.googleapis.com/css2?family=Open+Sans:wght@400;600&display=swap"" rel=""stylesheet"" type=""text/css"">
              <style type=""text/css"">
                body {
                  margin: 0;
                  padding: 0;
                  font-family: 'Open Sans', sans-serif;
                  background: #E5E5E5;
                }
            
                table, td {
                  border-collapse: collapse;
                }
            
                .container {
                  width: 100%;
                  max-width: 500px;
                  margin: 70px 0px;
                  background-color: #fafafa;
                }
            
                .main-content {
                  padding: 48px 30px 40px;
                  color: #000000;
                }
            
                .button {
                  width: 100%;
                  background: #facc15;
                  text-decoration: none;
                  display: inline-block;
                  padding: 10px 0;
                  color: #000000;
                  font-size: 14px;
                  text-align: center;
                  font-weight: bold;
                  border-radius: 7px;
                }
            
                @media only screen and (max-width: 480px) {
                  .container {
                    width: 80% !important;
                  }
            
                  .button {
                    width: 50% !important;
                  }
                }
              </style>
            </head>
            
            <body>
              <table width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"" align=""center"" bgcolor=""#1c1917"">
                <tbody>
                  <tr>
                    <td align=""center"" style=""padding-top: 40px; font-size: 24px; font-weight: bold; color: #fafafa; font-family: 'Larque BC', 'Open Sans', sans-serif;"">
                       SignSafe
                    </td>
                  </tr>
                  <tr>
                    <td valign=""top"" align=""center"">
                      <table class=""container"" width=""600"" cellspacing=""0"" cellpadding=""0"" border=""0"">
                        <tbody>
                          <tr>
                            <td class=""main-content"">
                              <table width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"">
                                <tbody>
                                  <tr>
                                    <td style=""padding: 0 0 24px; font-size: 18px; line-height: 150%; font-weight: bold;"">
                                      Verify your email
                                    </td>
                                  </tr>
                                  <tr>
                                    <td style=""padding: 0 0 10px; font-size: 14px; line-height: 150%;"">
                                      You are just one step away to verify your account for this email: <span style=""color: #ca8a04;"">{{email}}</span>.
                                    </td>
                                  </tr>
                                  <tr>
                                    <td style=""padding: 0 0 16px; font-size: 14px; line-height: 150%; font-weight: 700;"">
                                      Use below OTP to verify your account.
                                    </td>
                                  </tr>
                                  <tr>
                                    <td style=""padding: 0 0 24px;"">
                                      <p class=""button"" >{{otp}}</p>
                                    </td>
                                  </tr>
                                  <tr>
                                    <td style=""padding: 0 0 10px; font-size: 14px; line-height: 150%;"">
                                      For security purposes, this OTP will expire in 10 minutes.
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </td>
                  </tr>
                </tbody>
              </table>
            </body>
            </html>
         ";

        public const string PASSWORD_RESET = @"
            <!DOCTYPE html>
            <html xmlns=""http://www.w3.org/1999/xhtml"">
            
            <head>
              <title>Password Reset</title>
              <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
              <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"">
              <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
              <link href=""https://fonts.googleapis.com/css2?family=Open+Sans:wght@400;600&display=swap"" rel=""stylesheet"" type=""text/css"">
              <style type=""text/css"">
                body {
                  margin: 0;
                  padding: 0;
                  font-family: 'Open Sans', sans-serif;
                  background: #E5E5E5;
                }
            
                table, td {
                  border-collapse: collapse;
                }
            
                .container {
                  width: 100%;
                  max-width: 500px;
                  margin: 70px 0px;
                  background-color: #fafafa;
                }
            
                .main-content {
                  padding: 48px 30px 40px;
                  color: #000000;
                }
            
                .button {
                  width: 100%;
                  background: #facc15;
                  text-decoration: none;
                  display: inline-block;
                  padding: 10px 0;
                  color: #000000;
                  font-size: 14px;
                  text-align: center;
                  font-weight: bold;
                  border-radius: 7px;
                }
            
                @media only screen and (max-width: 480px) {
                  .container {
                    width: 80% !important;
                  }
            
                  .button {
                    width: 50% !important;
                  }
                }
              </style>
            </head>
            
            <body>
              <table width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"" align=""center"" bgcolor=""#1c1917"">
                <tbody>
                  <tr>
                    <td align=""center"" style=""padding-top: 40px; font-size: 24px; font-weight: bold; color: #fafafa; font-family: 'Larque BC', 'Open Sans', sans-serif;"">
                       SignSafe
                    </td>
                  </tr>
                  <tr>
                    <td valign=""top"" align=""center"">
                      <table class=""container"" width=""600"" cellspacing=""0"" cellpadding=""0"" border=""0"">
                        <tbody>
                          <tr>
                            <td class=""main-content"">
                              <table width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"">
                                <tbody>
                                  <tr>
                                    <td style=""padding: 0 0 24px; font-size: 18px; line-height: 150%; font-weight: bold;"">
                                      Forgot your password?
                                    </td>
                                  </tr>
                                  <tr>
                                    <td style=""padding: 0 0 10px; font-size: 14px; line-height: 150%;"">
                                      We received a password reset request for your account: <span style=""color: #ca8a04;"">{{email}}</span>.
                                    </td>
                                  </tr>
                                  <tr>
                                    <td style=""padding: 0 0 16px; font-size: 14px; line-height: 150%; font-weight: 700;"">
                                      Use the OTP below to reset the password.
                                    </td>
                                  </tr>
                                  <tr>
                                    <td style=""padding: 0 0 24px;"">
                                      <p class=""button"" >{{otp}}</p>
                                    </td>
                                  </tr>
                                  <tr>
                                    <td style=""padding: 0 0 10px; font-size: 14px; line-height: 150%;"">
                                      For security purposes, this OTP will expire in 10 minutes.
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </td>
                  </tr>
                </tbody>
              </table>
            </body>
            </html>
         ";
    }
}
