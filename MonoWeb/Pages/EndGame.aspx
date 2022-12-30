<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndGame.aspx.cs" Inherits="MonoWeb.EndGame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Monopoly Web - Game Over</title>
        <link href="../Style.css" rel="stylesheet" type="text/css" /> 
        <link href="../Content/bootstrap.min.css" rel="stylesheet" type="text/css" />  
    </head>
    <body class="body-home">
        <form id="form1" runat="server">     
            <div class="container-fluid sticky-footer-wrapper">          
                <div class="row no-gutters vh90">
                    <div class="col-sm-12">
                        <div class="row">
                            <div class="col-sm-12">
                                <h1 class="h1-responsive font-weight-bold text-center text-orange padding-top-50">Game Over</h1>
                            </div>
                        </div>

                        <div class="row text-center padding-top-100 justify-content-center align-content-center">
                            <div class="col-sm-12">
                                <div class="row">
                                    <div class="col-sm-3"></div>
                                    <div class="col-sm-6 align-content-center justify-content-center">
                                        <asp:Button ID="Button1" runat="server" Text="Return To Home" class="btn btn-dark btn-custom btn-block opacity-7 on-top" OnClick="Button1_Click1" />
                                    </div>
                                    <div class="col-sm-3"></div>
                                </div>                                
                            </div>
                        </div>
                    </div>
                </div>
                <div>
                    <asp:Label ID="Label1" runat="server" Text="" Visible="false"></asp:Label>
                </div>
            </div>
         </form>
    </body>
</html>
