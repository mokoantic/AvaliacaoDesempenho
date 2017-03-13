<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/PaginaBase.Master" AutoEventWireup="true" CodeBehind="Quadrantes.aspx.cs" Inherits="AvaliacaoDesempenho.WebForms.Quadrantes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-sm-9 col-sm-offset-3 col-lg-10 col-lg-offset-2 main">

        <div class="row">
            <ol class="breadcrumb">
                <li><i class="fa fa-list" style="color:#30a5ff;"></i> Quadrantes</li>
            </ol>
        </div>				
		
		<div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">Definições</div>
                    <div class="panel-body">
                        <div class="col-md-3">
                            <div role="form">
                                <div class="form-group">
                                    <label>Performance Individual:</label>
                                    <asp:TextBox ID="TBPerformance" runat="server" class="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>Competências:</label>
                                    <asp:TextBox ID="TBCompetencias" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div role="form">
                                <div class="form-group">
                                    <label>Experiência na posição:</label>
                                    <asp:TextBox ID="TBExpPosicao" runat="server" class="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>Experiência de trabalho:</label>
                                    <asp:TextBox ID="TBExpTrabalho" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <%--<button type="submit" class="btn btn-primary pull-right">Guardar</button>--%>
                        </div>
                        <div class="col-md-6" style="padding-top:100px;">
                            <button type="submit" class="btn btn-primary pull-right">Guardar</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
			<%--<div class="col-lg-12">
				<h2>Overview</h2>
			</div>	--%>
			<div class="col-md-6">
				<div class="panel panel-blue">
					<div class="panel-heading dark-overlay">Performance Individual</div>
					<div class="panel-body">
						<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque ut ante in sapien blandit luctus sed ut lacus. Phasellus urna est, faucibus nec ultrices placerat, feugiat et ligula. Donec vestibulum magna a dui pharetra molestie. Fusce et dui urna.</p>
					</div>
				</div>
			</div>
			<div class="col-md-6">
				<div class="panel panel-teal">
					<div class="panel-heading dark-overlay">Competências, Atitudes e Comportamentos</div>
					<div class="panel-body">
						<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque ut ante in sapien blandit luctus sed ut lacus. Phasellus urna est, faucibus nec ultrices placerat, feugiat et ligula. Donec vestibulum magna a dui pharetra molestie. Fusce et dui urna.</p>
					</div>
				</div>
			</div>			
			<div class="col-md-6">
				<div class="panel panel-orange">
					<div class="panel-heading dark-overlay">Experiência na posição</div>
					<div class="panel-body">
						<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque ut ante in sapien blandit luctus sed ut lacus. Phasellus urna est, faucibus nec ultrices placerat, feugiat et ligula. Donec vestibulum magna a dui pharetra molestie. Fusce et dui urna.</p>
					</div>
				</div>
			</div>			
			<div class="col-md-6">
				<div class="panel panel-red">
					<div class="panel-heading dark-overlay">Experiência de trabalho</div>
					<div class="panel-body">
						<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque ut ante in sapien blandit luctus sed ut lacus. Phasellus urna est, faucibus nec ultrices placerat, feugiat et ligula. Donec vestibulum magna a dui pharetra molestie. Fusce et dui urna.</p>
					</div>
				</div>
			</div>
		</div>		
	</div>

</asp:Content>
