<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ADateSelector.ascx.cs"  Inherits="ADateSelector" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<table border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td valign="middle">
            <asp:TextBox runat="server" ID="txtDate" CssClass="Textbox" Columns="7" />
        </td>
        <td valign="middle">
            <asp:ImageButton runat="Server" ID="imgCalendar" ImageUrl="~/Images/Calendar.png"
                AlternateText="Click to show calendar" /><asp:RequiredFieldValidator ID="vldDate"
                    runat="server" ErrorMessage="*" ControlToValidate="txtDate" Display="Dynamic" Enabled="False"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:PlaceHolder ID="PlaceHolder1" runat="server">
                <ajaxToolkit:CalendarExtender Format="dd/MM/yyyy" ID="calendarButtonExtender" runat="server"
                    TargetControlID="txtDate" PopupButtonID="imgCalendar" />
                <ajaxToolkit:MaskedEditExtender CultureName="pt-BR" ID="MaskedEditExtender5" runat="server"
                    TargetControlID="txtDate" Mask="99/99/9999" AutoComplete="true" MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date"
                    DisplayMoney="Left" AcceptNegative="Left"/>
                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator5" runat="server" ControlExtender="MaskedEditExtender5"
                    ControlToValidate="txtDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                    Display="Dynamic" TooltipMessage="" EmptyValueBlurredText="" InvalidValueBlurredMessage="Date is invalid"
                    ValidationGroup="MKE" />
            </asp:PlaceHolder>
        </td>
    </tr>
</table>
