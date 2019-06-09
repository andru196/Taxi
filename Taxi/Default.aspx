<%@ Page Title="Меню диспетчера" ValidateRequest="false" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Taxi._Default" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <table>
            <thead>
                <tr>
                    <th>№</th><th>Заказчик</th><th>Машина</th>
                    <th>Водитель</th><th>Маршрут</th><th>Дата заказа

                                                     </th>
                </tr>
            </thead>
            <tbody>
                <%
                    int count = 1;
                    foreach (var order in ordList)
                    {
                        
                %>
                    <tr>
                        <%if (order.Price > 0)
                            {
                        %>
                        <td><%= order.Id %></td><td><%= order.Customer.ToString("<br>") %></td><td><%= order?.Car?.ToString("<br>") %></td>
                        <td><%= order?.Driver?.ToString("<br>") %></td><td><%= order.Way.ToString("<br>") %></td><td><%= order.Date.Date.ToString() %></td>
                    
                        <%
                            }
                            else
                            {%>
                        <td><%= order.Id %></td><td><%= order.Customer.ToString("<br>") %></td>
                         <th colspan="2">           
                        <asp:DropDownList runat="server" ID="d2a">
                           
                        </asp:DropDownList></th>
                        
                        <th>Стоимость:<br><asp:TextBox runat="server"  ID="price"></asp:TextBox></th>
                    

                <%                     

                        
                    }
                %>
                        </tr>
                <%
                        count++;
                    }
                %>
            </tbody>
        </table>
    </div>

</asp:Content>
