<%@ Page Title="Назначение водителей на авто" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  CodeBehind="Insert D2A.aspx.cs" Inherits="Taxi.Insert_D2A" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <div>
            <table>
                <tr>
                    <td>Машина</td><td>Водитель</td><td></td>
                </tr>
                <tr>
                    <td> <asp:DropDownList runat="server" ID="AutoId"></asp:DropDownList></td>
                    <td><asp:DropDownList runat="server" ID="DriverId"></asp:DropDownList></td>
                    <td><asp:Button runat="server" Text="Добавить" OnClick="btnAddNewD2A" /></td>
                </tr>
                 <%
                    int count = 1;
                    foreach (var d2a in d2aList)
                    {
                        
                %>
                    <tr>
                    <td><%= d2a.Id %></td><td><%= d2a.Driver.Name %></td><td><%= d2a.Auto.ToString() %></td>   
                      <td><%= d2a.Date.ToShortDateString() %></td>
                    </tr>
                <%
                        count++;
                    }
                %>
            </table>


        </div>
</asp:Content>
