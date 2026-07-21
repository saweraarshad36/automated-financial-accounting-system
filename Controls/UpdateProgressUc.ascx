<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/Controls/UpdateProgressUc.ascx.cs"
    Inherits="UpdateProgressUc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxControlToolkit" %>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" OnLoad="UpdateProgress1_Load"
    DisplayAfter="100">
    <ProgressTemplate>
        <table>
            <tr>
                <td align="center">
                    <asp:ImageButton ImageUrl="~/Images/Loading.gif" ID="ImageButton1" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="Label1" runat="server" ForeColor="Tomato" Text="Loading, please wait..."
                        CssClass="loadingLabel"></asp:Label>
                </td>
            </tr>
        </table>
        <AjaxControlToolkit:AlwaysVisibleControlExtender ID="UpdateProgressVisibilityExtender"
            runat="server" TargetControlID="UpdateProgress1" VerticalSide="Middle" VerticalOffset="0"
            HorizontalSide="Center" HorizontalOffset="0" ScrollEffectDuration=".1" />
    </ProgressTemplate>
</asp:UpdateProgress>
