<%@ Page Title="EditInformation" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditInformation.aspx.cs" Inherits="USADataRoundabout.EditInformation" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
    .big-flex {
        display: flex;
        flex-direction: row;
        justify-content: space-evenly;
        padding-top: 50px;
    }
    .flex-container {
        display: flex;
        flex-direction: column;
        align-items: start;
    }
</style>
    <div class="big-flex">
        <div class="flex-container" runat="server">
            <dl>
                <dt>Add New Facility/Location</dt>
                <dd><input type="text" ID="txtNewLocation" runat="server" /></dd>
            </dl>
        </div>
        <div class="flex-container">
            <button type="submit" runat="server" id="Button1" onserverclick="btnSubmit_Click">Submit</button>
        </div>
    </div>
    <br />
    <br />
</asp:Content>
