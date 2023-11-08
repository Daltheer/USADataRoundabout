<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SubmitComplaint.aspx.cs" Inherits="USADataRoundabout.About" %>

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
        <div class="flex-container">
            <p>Location:</p>
            <input type="text" id="txtLocation"  runat="server"/>
            <p>Facility:</p>
            <select id="ddlFacility" runat="server">
                <option value="ShelbyHall">Shelby Hall</option>
                <option value="AdminBldg">Administration Building</option>
            </select>
            <p>Complaint Impact:</p>
            <select id="ddlComplaintImpact" runat="server">
                <option value="high">High</option>
                <option value="medium">Medium</option>
                <option value="low">Low</option>
            </select>
            <p>Related Schools:</p>
            <input type="text" id="txtRelSchools"  runat="server"/>
            <p>Protected Classes?   <input type="checkbox" runat="server" id="chkProtClass" onserverchange="chkProtClass_OnChange" onserverselect="chkProtClass_OnChange" onserverfocus="chkProtClass_OnChange" onserverclick="chkProtClass_OnChange" /></p>
            <p>
                <asp:ListBox ID="lstProtList" runat="server"></asp:ListBox>
                <asp:ListBox ID="lstProtSel" runat="server"></asp:ListBox>
            </p>
            <p>Additional Notes:</p>
            <input type="text" id="txtAddNotes" runat="server" />
            <p>Resolved?    <input type="checkbox" id="chkResolved" runat="server" /></p>
        </div>
        <div class="flex-container">
            <p>Date of Incidence:</p>
            <input type="date" id="dtDate" runat="server" />
            <p>Source of Complaint:</p>
            <input type="text" id="txtSourceComplaint" runat="server" />
            <p>Those Involved in the Complaint:</p>
            <input type="text" id="txtInvolved" runat="server" />
            <p>Action Taken:</p>
            <input type="text" id="txtActionTaken" runat="server" />
            <p></p>
            <p></p>
            <p></p>
            <button type="submit" runat="server" id="btnSubmit" onserverclick="btnSubmit_Click">Submit</button>
            </div>
        <div class="flex-container">
            <p>Who Submitted?</p>
            <input type="text" id="txtWhoSubm" runat="server" />
        </div>
    </div>
</asp:Content>
