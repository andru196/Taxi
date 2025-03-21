﻿<%@ Page Title="Меню диспетчера" ValidateRequest="false" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Taxi._Default" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <script>
            $(document).ready(function () {
                $("#<%= newIdInput.ClientID%>").attr('readonly', true);
            $("#<%= addOrd.ClientID%>").attr('disabled', true);
        });
            function checkParams() {
                var From = $('#<%= newFrom.ClientID%>').val();
                var To = $('#<%= newTo.ClientID %>').val();
                var phone = $('#<% = newPhone.ClientID%>').val();
                var name = $('#<%= newName.ClientID %>').val();

                if (name.length != 0 && From.length != 0 && phone.length != 0 && To.length != 0) {
                    $('#<% = addOrd.ClientID %>').removeAttr('disabled');
                } else {
                    $('#<% = addOrd.ClientID %>').attr('disabled', 'disabled');
                }
            }  
    </script>
    <div class="jumbotron">
        

        <table>
            <thead>
                <tr>
                    <th>Тип изменения</th><th>ID</th><th colspan="2">Выберите машину</th><th>Стоимость</th>

                </tr>
                </thead>
                <tr>
                <td><asp:DropDownList runat="server" ID="newActionType" OnSelectedIndexChanged="selActionType" AutoPostBack="true">
                            <asp:ListItem Value="1">Создать </asp:ListItem>
                            <asp:ListItem Value="2">Редактировать</asp:ListItem>
                        </asp:DropDownList></td>
                <td><asp:TextBox runat="server" onkeyup="checkParams()"    ID="newIdInput">
                    </asp:TextBox>
                    <asp:DropDownList runat="server" Visible="false" OnSelectedIndexChanged="selId" AutoPostBack="true" ID="newId">
                        </asp:DropDownList></td>
                </td>
                <td colspan="2">           
                        <asp:DropDownList runat="server" ID="newd2a">
                           
                        </asp:DropDownList></td>
                <td><asp:TextBox runat="server" onkeyup="checkParams()"  ID="newPrice"></asp:TextBox></td>
                    </tr>
            <thead>
                <tr>
                    <th>Откуда</th><th>Куда</th><th>Телефон</th><th>Имя заказчика</th><th>Управление</th>
                </tr>
            </thead>
            <tr>
                 <td><asp:DropDownList runat="server" ID="newFrom">  </asp:DropDownList></td>
                 <td><asp:DropDownList runat="server" ID="newTo">  </asp:DropDownList></td>
                 <td><asp:TextBox runat="server" onkeyup="checkParams()" TextMode="Phone" ID="newPhone"></asp:TextBox></td>
                 <td><asp:TextBox runat="server" onkeyup="checkParams()" ID="newName"></asp:TextBox></td>
                <td><asp:Button runat="server" Text="Сохранить" OnClick="btnAddNewOrder" ID="addOrd"/></td>
            </tr>
            <tr>
                <td colspan="3"> <asp:TextBox  onkeyup="checkParams()" TextMode="MultiLine" Columns="90" Rows="6" ID="newExtra" runat="server"></asp:TextBox></td>
                <td colspan="2">           
                        <asp:DropDownList runat="server" ID="newRadio">
                           
                        </asp:DropDownList></td>
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
                <td><asp:Button runat="server"   id="btnFilter" Text ="Применить фильтр" CssClass="btn btn-primary" OnClick="btnFilter_Click"/></td>
            </tr>
        </table>
        <script>
            function EditOrd(a) {
                if ($("#<%= newActionType.ClientID %>").val() == 2) {
                    $("#<%= newId.ClientID %>").val(a.childNodes[1].innerHTML).trigger('change');
                }
            }
        </script>      
        <table class="ordlist">
            <thead>
                <tr>
                    <th><asp:Button Text="№" runat="server" OnClick="btnSort_Click"/></th><th><asp:Button Text="Заказчик" OnClick="btnSort_Click" runat="server"/></th>
                    <th><asp:Button Text="Машина" runat="server" OnClick="btnSort_Click"/></th><th><asp:Button Text="Водитель" runat="server" OnClick="btnSort_Click"/></th>
                    <th><asp:Button Text="Маршрут" runat="server" OnClick="btnSort_Click"/></th><th><asp:Button Text="Дата заказа" runat="server" OnClick="btnSort_Click"/></th>
                    <th><asp:Button Text="Сумма" runat="server" OnClick="btnSort_Click"/></th>
                </tr>
            </thead>
            <tbody>
                <%
                    int count = 1;
                    foreach (var order in ordList)
                    {
                        
                %>
                    <tr onclick="EditOrd(this)">
                        
                        <td><%= order.Id %></td><td><%= order.Customer.ToString("<br>") %></td><td><%= order?.d2a.Auto?.ToString("<br>") %></td>
                        <td><%= order?.d2a?.Driver?.ToString("<br>") %></td><td><%= order.Way.ToString("<br>") %></td><td><%= order?.d2a.Date.ToShortDateString() %></td><td><%= order?.Price.ToString() %></td>
                    
                      
                        </tr>
                <%
                        count++;
                    }
                %>
            </tbody>
            <asp:Button Text="Назад" runat="server" ID="prevButton" OnClick="btnPrev_Click"/> <asp:Button Text="Вперёд" runat="server" ID="newxtButton" OnClick="btnNext_Click"/>
        </table>
        <asp:Table id="tbl_footer" runat="server" Visible="false" >
            <asp:TableRow>
                <asp:TableCell>Водитель</asp:TableCell><asp:TableCell>Количество поездок</asp:TableCell>
                <asp:TableCell>Среднее количество поездок</asp:TableCell><asp:TableCell>xml</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ID="drInf"></asp:TableCell><asp:TableCell ID="drCount"></asp:TableCell>
                <asp:TableCell ID="drMiddle"></asp:TableCell><asp:TableCell ID="drXML"></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
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

        .ordlist td, .ordlist th {
            border-color: #c1c2c1;
            border-style: solid;
            border-width: 1px;
        }
        .ordlist tr:hover{
            background-color: aliceblue;
        }
    </style>
    <asp:HiddenField runat="server" ID="sortType"/>
    <asp:HiddenField runat="server" ID="selPage"/>
</asp:Content>
