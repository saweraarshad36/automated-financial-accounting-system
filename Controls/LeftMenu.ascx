<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LeftMenu.ascx.cs" Inherits="Controls_LeftMenu" %>

<script language="javascript" type="text/javascript" id="DashScripts">
    function callAlert(MsgStr) {
        var ans;
        ans = window.confirm(MsgStr);
        if (ans) {
            return true;
        }
        else {
            return false;
        }
    }
    function changecolor(color) {
        var el = event.srcElement
        if (el.tagName == "BtnTotal" && el.type == "button")
            event.srcElement.style.backgroundColor = color
    }

    function change1(e) {
        //event.srcElement.style.backgroundColor='RoyalBlue'
        if (window.event) {
            //code for ie  
            window.event.srcElement.style.backgroundColor = "#339A99"
            window.event.srcElement.style.color = "Black"

        }
        else {
            callAlert(this.style.backgroundColor)
            //code for firefox
            //e.target.style.background.color="RoyalBlue" 
            this.style.backgroundColor = '#CCFF99';
        }
    }
    function change2() {
        if (window.event) {
            //    event.srcElement.style.backgroundColor='#99CCFF'
            event.srcElement.style.backgroundColor = '#339A99'
            event.srcElement.style.color = 'White'
            window.event.srcElement.style.color = "White"
        }
        else {
            this.style.backgroundColor = '#6495ff'
        }
    }


</script>
<asp:UpdatePanel ID="UpdatePanelHdr3" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <table runat="server"  id="tblLeft" style="width: 100%; height: 400px; background-color: #339A99">
            
            
            <tr>
                <td style="ruby-align: center; height: 30px">
                    

                    
                                        
                    


                    
                    
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top; height: 165px; width: 100%">
                    <asp:DataGrid ID="dgMenu" runat="server" AutoGenerateColumns="False" BorderStyle="None" BorderWidth="0px" CellPadding="3" Font-Bold="False" Font-Italic="False" Font-Names="Calibri" Font-Overline="False" Font-Size="Large" Font-Strikeout="False" Font-Underline="False" GridLines="Vertical" OnItemCommand="dgMenu_ItemCommand" ShowHeader="False" Style="vertical-align: top" Width="100%">
                        <FooterStyle ForeColor="Black" />
                        <SelectedItemStyle BackColor="#F2AD46" Font-Bold="True" ForeColor="White" />
                        <PagerStyle ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                        <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <ItemStyle Font-Size="20pt" ForeColor="Black" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <HeaderStyle Font-Bold="True" ForeColor="White" font-size="10px"/>
                        <Columns>
                            <asp:BoundColumn DataField="MenuPath" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MenuId" Visible="False"></asp:BoundColumn>
                            <asp:HyperLinkColumn DataNavigateUrlField="MenuPath" DataTextField="MenuText" Visible="False">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="12pt" Font-Strikeout="False" Font-Underline="False" />
                            </asp:HyperLinkColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <a href='<%# DataBinder.Eval(Container, "DataItem.ImagePath") %>'>
                                        <asp:Image ID="Image1" runat="server" Height="20px" ImageAlign="Middle" ImageUrl='<%# DataBinder.Eval(Container, "DataItem.ImagePath") %>' Width="0px" Visible="False" />
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkPath" runat="server" Font-Bold="False" Font-Size="12pt" ForeColor="White"><%# DataBinder.Eval(Container, "DataItem.MenuText") %></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
        </table>

    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnHide" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>
