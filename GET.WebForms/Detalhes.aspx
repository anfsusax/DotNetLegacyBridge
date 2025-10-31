<%@ Page Title="Detalhes do Cliente" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Detalhes.aspx.cs" Inherits="GET.WebForms.Detalhes" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-3">
        <div class="card shadow-sm">
            <div class="card-header d-flex justify-content-between align-items-center">
                <span class="fw-semibold">Detalhes do Cliente</span>
                <a runat="server" href="~/" class="btn btn-sm btn-outline-secondary">Voltar</a>
            </div>
            <div class="card-body">
                <div class="row g-3">
                    <div class="col-md-6">
                        <div class="border rounded p-3">
                            <h6 class="text-secondary">Dados Pessoais</h6>
                            <div class="row">
                                <div class="col-6"><span class="text-muted">Nome</span><br /><asp:Label ID="lblNome" runat="server" Text="-" /></div>
                                <div class="col-6"><span class="text-muted">CPF</span><br /><asp:Label ID="lblCpf" runat="server" Text="-" /></div>
                                <div class="col-6 mt-2"><span class="text-muted">Sexo</span><br /><asp:Label ID="lblSexo" runat="server" Text="-" /></div>
                                <div class="col-6 mt-2"><span class="text-muted">Estado Civil</span><br /><asp:Label ID="lblEstadoCivil" runat="server" Text="-" /></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="border rounded p-3">
                            <h6 class="text-secondary">Endereço</h6>
                            <div class="row">
                                <div class="col-6"><span class="text-muted">CEP</span><br /><asp:Label ID="lblCep" runat="server" Text="-" /></div>
                                <div class="col-6"><span class="text-muted">UF</span><br /><asp:Label ID="lblUf" runat="server" Text="-" /></div>
                                <div class="col-12 mt-2"><span class="text-muted">Rua</span><br /><asp:Label ID="lblRua" runat="server" Text="-" /></div>
                                <div class="col-6 mt-2"><span class="text-muted">Número</span><br /><asp:Label ID="lblNumero" runat="server" Text="-" /></div>
                                <div class="col-6 mt-2"><span class="text-muted">Bairro</span><br /><asp:Label ID="lblBairro" runat="server" Text="-" /></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card-footer d-flex justify-content-end gap-2">
                <a runat="server" href="~/" class="btn btn-secondary">Fechar</a>
            </div>
        </div>
    </div>
</asp:Content>
