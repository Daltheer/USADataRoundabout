﻿<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SubmitComplaint.aspx.cs" Inherits="USADataRoundabout.About" %>

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
            <dl>
                <dt>Location of Incident:</dt>
                <dd><asp:DropDownList id="ddlFacility" runat="server">
                    
                </asp:DropDownList></dd>
            </dl>
            <dl>
                <dt>Complaint Level:</dt>
                <dd><select id="ddlComplaintImpact" runat="server">
                    <option value="High">High</option>
                    <option value="Medium">Medium</option>
                    <option value="Low">Low</option>
                </select></dd>
            </dl>
            <dl>
                <dt>Does the incident involve any of the following</dt>
                <dt>protected classes? Check all that apply.</dt>
                <dd><input type="checkbox" id="chkRace" runat="server" />   Race</dd>
                <dd><input type="checkbox" id="chkReligion" runat="server" />   Religion</dd>
                <dd><input type="checkbox" id="chkSex" runat="server" />   Sex</dd>
                <dd><input type="checkbox" id="chkPregnancy" runat="server" />   Pregnancy</dd>
                <dd><input type="checkbox" id="chkSexOrient" runat="server" />   Sexual Orientation</dd>
                <dd><input type="checkbox" id="chkGenderIden" runat="server" />   Gender Identity</dd>
                <dd><input type="checkbox" id="chkNational" runat="server" />   National Origin</dd>
                <dd><input type="checkbox" id="chkAge" runat="server" />   Age (40 or Older)</dd>
                <dd><input type="checkbox" id="chkDisability" runat="server" />   Disability</dd>
                <dd><input type="checkbox" id="chkGenetic" runat="server" />  Genetic Information</dd>
            </dl>
            <dl>
                <dt>Additional Notes:</dt>
                <dd><input style="width: 300px; height:200px;" type="text" id="txtAddNotes" runat="server" /></dd>
            </dl>
        </div>
        <div class="flex-container">
            <dl>
                <dt>Date of Incident:</dt>
                <dd><input type="date" id="dtDate" runat="server" /></dd>
            </dl>
            <dl>
                <dt>Source of Complaint:</dt>
                <dd><select id="ddlSource" runat="server">
                    <option value="Student">Student</option>
                    <option value="Faculty">Faculty</option>
                    <option value="Staff">Staff</option>
                    <option value="Alumni">Alumni</option>
                    <option value="Class">Class</option>
                    <option value="Campus Guest">Campus Guest</option>
                    <option value="Community Member">Community Member</option>
                </select></dd>
            </dl>
            <dl>
                <dt>Those Involved in the Complaint: (check all that apply)</dt>
                <dd><input type="checkbox" id="chkStudent" runat="server" />   Student</dd>
                <dd><input type="checkbox" id="chkFaculty" runat="server" />   Faculty</dd>
                <dd><input type="checkbox" id="chkStaff" runat="server" />   Staff</dd>
                <dd><input type="checkbox" id="chkAlumni" runat="server" />   Alumni</dd>
                <dd><input type="checkbox" id="chkCampusGuest" runat="server" />   Campus Guest</dd>
                <dd><input type="checkbox" id="chkCommunityMember" runat="server" />   Community Member</dd>
            </dl>
            <dl>
                <dt>Action Taken:</dt>
                <dd><input style="width: 300px; height:200px;" type="text" id="txtActionTaken" runat="server" /></dd>
            </dl>
            <p>Resolved?    <input type="checkbox" id="chkResolved" runat="server" /></p>
        </div>
    </div>
    <div class="big-flex">
        <button type="submit" runat="server" id="btnSubmit" onserverclick="btnSubmit_Click">Submit</button>
        <!--<var id="output" runat="server">imhere</var>-->
    </div>
</asp:Content>
