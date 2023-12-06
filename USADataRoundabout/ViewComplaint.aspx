<%@ Page Title="ViewComplaint" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewComplaint.aspx.cs" Inherits="USADataRoundabout.ViewComplaint" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
    .big-flex {
        display: flex;
        flex-direction: row;
        justify-content: space-evenly;
        padding-top: 50px;
    }
    .small-flex {
        display: flex;
        flex-direction: column;
        align-items: start;
    }
</style>
    <div class="big-flex">
        <var id="complaintIDTitle" runat="server"></var>
        <var id="originalDate" runat="server"></var>
        <button type="submit" runat="server" id="btnBookmark" onserverclick="btnBookmark_Click">Add to Bookmarks</button>
        <button type="submit" runat="server" id="btnRemoveBookmark" onserverclick="btnRemoveBookmark_Click">Remove from Bookmarks</button>
        <button type="submit" runat="server" id="btnEdit" onserverclick="btnEdit_Click">Edit Complaint</button>
        <button type="submit" runat="server" id="btnCancel" onserverclick="btnCancel_Click">Cancel Edit</button>
    </div>
    <div class="big-flex" id="viewSection" runat="server">
        <div class="flex-container">
            <dl>
                <dt>Location of Incident:</dt>
                <dd><input type="text" id="txtFacilityView" runat="server" readonly /></dd>
            </dl>
            <dl>
                <dt>Complaint Level:</dt>
                <dd><input type="text" id="txtCompImpactView" runat="server" readonly /></dd>
            </dl>
            <dl>
                <dt>Involved Protected Classes:</dt>
                <dd><ul id="ulProtClassView" runat="server"></ul></dd>
            </dl>
            <dl>
                <dt>Additional Notes:</dt>
                <dd><input type="text" id="txtAddNotesView" runat="server" readonly /></dd>
            </dl>
        </div>
        <div class="flex-container">
            <dl>
                <dt>Date of Incident:</dt>
                <dd><input type="text" id="txtDateIncView" runat="server" readonly /></dd>
            </dl>
            <dl>
                <dt>Source of Complaint:</dt>
                <dd><input type="text" id="txtSourceCompView" runat="server" readonly/></dd>
            </dl>
            <dl>
                <dt>Those Involved in the Complaint:</dt>
                <dd><ul id="ulStakeholdersView" runat="server"></ul></dd>
            </dl>
            <dl>
                <dt>Action Taken:</dt>
                <dd><input type="text" id="txtActionTakenView" runat="server" readonly /></dd>
            </dl>
            <p>Resolved?    <input type="checkbox" id="chkResolvedView" runat="server" onclick="return false;" /></p>
        </div>
    </div>
    <div class="big-flex" id="editSection" runat="server">
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
    <div class="big-flex" id="editButton" runat="server">
        <button type="submit" runat="server" id="btnSubmit" onserverclick="btnSubmit_Click">Submit</button>
    </div>
</asp:Content>
