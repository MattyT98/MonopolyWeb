<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MonoWeb.Default" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Monopoly Web - Game</title>
        <link href="../Style.css" rel="stylesheet" type="text/css" /> 
        <link href="../Content/bootstrap.min.css" rel="stylesheet" type="text/css" />   
    </head>
    <body id="body">
        <form id="form2" runat="server">
            <div class="container-fluid sticky-footer-wrapper"> 
                <div class="row">
                    <h1 class="h1-responsive font-weight-bold text-center text-orange padding-bottom-10 underline-row">Monopoly Web</h1>
                </div>
                <div class="row padding-top-30">
                    <div class="col-sm-6"> 
                        <div id="MainBoard">
                            <div id=" boardimg"class="row">
                                <div class="row">
                                <asp:Image ID="Image43" runat="server" src="../Images/board.png" class="boardimg"/>
                                      <%--Characters--%>
                                    <asp:Image ID="P1Character" runat="server" src="../Images/(Character) Boat.png" class="CharacterImage"/> 
                                    <asp:Image ID="P2Character" runat="server" src="../Images/(Character) Car.png" class="CharacterImage"/>
                                    <asp:Image ID="P3Character" runat="server" src="../Images/(Character) Boot.png" class="CharacterImage"/>
                                    <asp:Image ID="P4Character" runat="server" src="../Images/(Character) Dog.png" class="CharacterImage"/>
                                    <asp:Label ID="Label14" runat="server" Text="Label" Visible="false"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div id="GameDetails">
                            <div class="row GameDetails">
                                <div id="playerStats" class="col-sm-12 font-weight-bold text-center text-orange padding-top-50">
                                    <div class="row">
                                        <asp:Label ID="Label1" runat="server" Text="Player Stats" Font-Size="20pt" style="text-align:center; padding: 10px; text-decoration:underline;"></asp:Label>
                                    </div>
                                    <div class="row">
                                        <asp:Label ID="Label11" runat="server" Class="playerStats GameInfoText" Text="Player: &nbsp"></asp:Label>
                                        <asp:Label ID="Label12" runat="server" Class="playerStats GameInfoText" Text="1"></asp:Label>
                                    </div>
                                    <div class="row">
                                        <asp:Label ID="Label2" runat="server" Class="playerStats GameInfoText" Text="Money: &nbsp"></asp:Label>
                                        <asp:Label ID="Label3" runat="server" Class="playerStats GameInfoText" Text="£1500"></asp:Label>
                                    </div>
                                    <div class="row">
                                        <asp:Label ID="Label4" runat="server" Class="playerStats GameInfoText" Text="Properties Owned: &nbsp"></asp:Label>
                                        <asp:Label ID="Label5" runat="server" Class="playerStats GameInfoText" Text="0"></asp:Label>
                                    </div>
                                    <div class="row">
                                        <asp:Label ID="Label6" runat="server" Class="playerStats GameInfoText" Text="Jail Out Of Free: &nbsp"></asp:Label>                               
                                        <asp:Label ID="Label7" runat="server"  Class="playerStats GameInfoText" Text="False"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div id="movement" class="col-sm-12 font-weight-bold text-center text-orange padding-top-50">
                                     <div class="row">
                                         <asp:Label ID="Label8" runat="server" Text="Movement" Font-Size="20pt" style="text-align:center; padding: 10px; text-decoration:underline;"></asp:Label>
                                    </div>
                                    <div class="row">
                                        <asp:Label ID="Label10" runat="server" Text="Current Space:" Class="Movement GameInfoText"></asp:Label>
                                        <asp:Label ID="Label13" runat="server" Text="Pass Go" Class="Movement GameInfoText"></asp:Label>
                                    </div>
                                    <div class="row padding-bottom-10 padding-top-10 text-center">
                                        <asp:Button ID="Button1" runat="server" Text="Roll" width="90px" Height="50px" Class="btn btn-dark btn-custom opacity-7 on-top" OnClick="Button1_Click"/>
                                    </div>
                                    <div class="row">
                                        <asp:Image ID="Image41" runat="server" class="dice"/>
                                        <asp:Image ID="Image42" runat="server" class="dice"/> 
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div id="propertyStats" class="col-sm-12 font-weight-bold text-center text-orange padding-top-50">
                                    <div class="row">
                                        <asp:Label ID="Label9" runat="server" Text="Property Management" Font-Size="20pt" style="text-align:center; padding: 10px; text-decoration:underline;"></asp:Label>
                                    </div>
                                    <div class="row">                                
                                        <asp:ListBox ID="ListBox1" style="min-width:30%;" runat="server"></asp:ListBox>
                                    </div>
                                      <div class="row">                                
                                        <asp:Button ID="Button2" class="btn btn-dark btn-custom opacity-7 on-top" style="min-width:30%;" runat="server" Text="Buy Property" OnClick="Button2_Click"/>
                                    </div>
                                </div>  
                                <div class="row">
                                    <div class="col-sm-12 font-weight-bold text-center text-orange padding-top-50">
                                        <asp:Button ID="Button3" class="btn btn-dark btn-custom opacity-7 on-top" runat="server" Text="End Turn" style="min-width:15%; min-height: 50px;" OnClick="Button3_Click" />
                                        <asp:Button ID="Button4" class="btn btn-dark btn-custom opacity-7 on-top" runat="server" Text="Save Game" style="min-width:15%; min-height: 50px;" OnClick="Button4_Click" />
                                    </div>  
                                </div>
                            </div>
                        </div>
                    </div>
                </div>                
                <div id="DatabaseDetails" class="row">
                    <div id="playerTable" class="col-sm-12">
                        <asp:Table ID="Table1" style="width:100%; height:100%; margin: auto" runat="server"></asp:Table>
                    </div>   
                </div>
            </div>
        </form>
    </body>
</html>

