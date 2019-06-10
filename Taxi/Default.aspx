<%@ Page Title="Меню диспетчера" ValidateRequest="false" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Taxi._Default" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">


        <table>
            <thead>
                <tr>
                    <th>Тип изменения</th><th>ID</th><th colspan="2">Выберите машину</th><th>Стоимость</th>

                </tr>
                </thead>
                <tr>
                <td><asp:DropDownList runat="server" ID="newActionType">
                            <asp:ListItem Value="1">Создать</asp:ListItem>
                            <asp:ListItem Value="2">Редактировать</asp:ListItem>
                        </asp:DropDownList></td>
                <td><asp:TextBox runat="server"  ID="newIdInput"></asp:TextBox></td>
                <td colspan="2">           
                        <asp:DropDownList runat="server" ID="newd2a">
                           
                        </asp:DropDownList></td>
                <td><asp:TextBox runat="server"  ID="newPrice"></asp:TextBox></td>
                    </tr>
            <thead>
                <tr>
                    <th>Откуда</th><th>Куда</th><th>Телефон</th><th>Имя заказчика</th><th>Управление</th>
                </tr>
            </thead>
            <tr>
                 <td><asp:TextBox runat="server"  ID="newFrom"></asp:TextBox></td>
                 <td><asp:TextBox runat="server"  ID="newTo"></asp:TextBox></td>
                 <td><asp:TextBox runat="server"  ID="newPhone"></asp:TextBox></td>
                 <td><asp:TextBox runat="server"  ID="newName"></asp:TextBox></td>
                <td><asp:Button runat="server" Text="Сохранить" OnClick="btnAddNewOrder" ID="addOrd"/></td>
            </tr>
        </table>
        <br />
        <br />         
        <hr />  
        <table>
            <tr>
                <td>Имя</td><td>Номер телефона</td><td>Номер документа</td>
            </tr>
            <tr>
                <td><asp:TextBox runat="server"  ID="fltName"></asp:TextBox></td>
                <td><asp:TextBox runat="server"  ID="fltPhone"></asp:TextBox></td>
                <td><asp:TextBox runat="server"  ID="fltDoc"></asp:TextBox></td>
                <td><asp:Button runat="server" id="btnFilter" Text ="Применить фильтр" CssClass="btn btn-primary" OnClick="btnFilter_Click"/></td>
            </tr>
        </table>
                        
        <table class="ordlist">
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
                        
                        <td><%= order.Id %></td><td><%= order.Customer.ToString("<br>") %></td><td><%= order?.Car?.ToString("<br>") %></td>
                        <td><%= order?.Driver?.ToString("<br>") %></td><td><%= order.Way.ToString("<br>") %></td><td><%= order.Date.Date.ToString() %></td>
                    
                      
                        </tr>
                <%
                        count++;
                    }
                %>
            </tbody>
        </table>
    </div>
    <style type="text/css">
        table th, table td {
          padding: 3px 5px;
          line-height: 1.3;
          text-align: left; }

        table tbody tr td {
          padding: 3px 9px;
          vertical-align: middle; }

        table tr th {
          width: 265px;
          text-align: left;
          vertical-align: middle; }

        .ordlist td, .ordlist th{
          border-color: #c1c2c1;
          border-style: solid;
          border-width: 1px; 

        }
    </style>
</asp:Content>
