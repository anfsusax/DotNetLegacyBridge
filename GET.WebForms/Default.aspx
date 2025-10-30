<%@ Page Title="Cadastro de Clientes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GET.WebForms._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Scripts de Validação -->
    <script type="text/javascript">
        function apenasNumeros(v) {
            return v.replace(/\D/g, '');
        }

        function mascaraCPF(v) {
            v = apenasNumeros(v);
            return v.replace(/(\d{3})(\d)/, "$1.$2")
                .replace(/(\d{3})(\d)/, "$1.$2")
                .replace(/(\d{3})(\d{1,2})$/, "$1-$2");
        }

        function aplicarMascara(o, f) {
            setTimeout(() => o.value = f(o.value), 1);
        }

        function validarCPF(source, args) {
            let cpf = apenasNumeros(document.getElementById('<%= txtCpf.ClientID %>').value);
            if (cpf.length !== 11 || /^(\d)\1+$/.test(cpf)) {
                args.IsValid = false;
                return;
            }

            let soma = 0, resto;
            for (let i = 1; i <= 9; i++) soma += parseInt(cpf.substring(i - 1, i)) * (11 - i);
            resto = (soma * 10) % 11;
            if (resto === 10 || resto === 11) resto = 0;
            if (resto !== parseInt(cpf.substring(9, 10))) { args.IsValid = false; return; }

            soma = 0;
            for (let i = 1; i <= 10; i++) soma += parseInt(cpf.substring(i - 1, i)) * (12 - i);
            resto = (soma * 10) % 11;
            if (resto === 10 || resto === 11) resto = 0;
            args.IsValid = resto === parseInt(cpf.substring(10, 11));
        }
    </script>

    <section class="page-hero mt-4 mb-4">
        <div class="d-flex flex-column flex-md-row align-items-start align-items-md-center justify-content-between gap-3">
            <div>
                <h2 class="page-title mb-1">Gestão de Clientes</h2>
                <p class="page-subtitle mb-0">Cadastre, edite e gerencie seus clientes com rapidez.</p>
            </div>
            <div>
                <button type="button" class="btn btn-light btn-lg" data-bs-toggle="modal" data-bs-target="#clienteModal">+ Novo Cliente</button>
            </div>
        </div>
    </section>

    <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="Cliente" runat="server" CssClass="text-danger" />

    <div class="mt-5 d-flex justify-content-center">
        <div class="card shadow-soft w-100" style="max-width: 1080px;">
            <div class="card-header bg-gradient-primary text-white d-flex justify-content-between align-items-center">
                <span class="fw-semibold">Lista de Clientes</span>
                <button type="button" class="btn btn-light btn-sm" data-bs-toggle="modal" data-bs-target="#clienteModal">Novo</button>
            </div>
            <div class="card-body p-0">
                <div class="table-responsive">
                    <asp:GridView ID="gdvClientes" runat="server" CssClass="table table-hover table-striped align-middle mb-0" AutoGenerateColumns="False" OnRowCommand="gdvClientes_RowCommand1">
                        <Columns>
                            <asp:BoundField DataField="Nome" HeaderText="Nome" />
                            <asp:BoundField DataField="Cpf" HeaderText="CPF" />
                            <asp:BoundField DataField="Sexo" HeaderText="Sexo" />
                            <asp:BoundField DataField="EstadoCivil" HeaderText="Estado Civil" />
                            <asp:TemplateField HeaderText="Ações">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEditar" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' runat="server" CssClass="btn btn-sm btn-outline-primary me-1">Editar</asp:LinkButton>
                                    <asp:LinkButton ID="lnkExcluir" CommandName="Excluir" CommandArgument='<%# Eval("Id") %>' runat="server" CssClass="btn btn-sm btn-outline-danger" OnClientClick="return handleDeleteClick(this, 'Excluir cliente?');">Excluir</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal: Cliente (Novo/Editar) -->
    <div class="modal fade" id="clienteModal" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-xl modal-dialog-scrollable">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Cadastro de Cliente</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="row mb-2">
                            <div class="col-md-3">
                                <label for="txtCpf">CPF*</label>
                                <asp:TextBox ID="txtCpf" CssClass="form-control" runat="server" 
                                    onkeypress="aplicarMascara(this, mascaraCPF)" MaxLength="14" placeholder="000.000.000-00"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCpf" ErrorMessage="Obrigatório" ForeColor="Red" ValidationGroup="Cliente" />
                                <asp:CustomValidator runat="server" ControlToValidate="txtCpf" ClientValidationFunction="validarCPF" ErrorMessage="CPF inválido" ValidationGroup="Cliente" ForeColor="Red" />
                            </div>

                            <div class="col-md-9">
                                <label for="txtNome">Nome*</label>
                                <asp:TextBox ID="txtNome" CssClass="form-control" runat="server" placeholder="Digite o nome completo"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNome" ErrorMessage="Obrigatório" ForeColor="Red" ValidationGroup="Cliente" />
                            </div>
                        </div>

                        <div class="row mb-2">
                            <div class="col-md-3">
                                <label for="txtRg">RG*</label>
                                <asp:TextBox ID="txtRg" CssClass="form-control" runat="server" placeholder="RG"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtRg" ErrorMessage="Obrigatório" ForeColor="Red" ValidationGroup="Cliente" />
                            </div>
                            <div class="col-md-3">
                                <label for="txtDataNascimento">Nascimento*</label>
                                <asp:TextBox ID="txtDataNascimento" type="date" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDataNascimento" ErrorMessage="Obrigatório" ForeColor="Red" ValidationGroup="Cliente" />
                            </div>
                            <div class="col-md-3">
                                <label for="ddlSexo">Sexo*</label>
                                <asp:DropDownList ID="ddlSexo" CssClass="form-select" runat="server">
                                    <asp:ListItem>Selecione</asp:ListItem>
                                    <asp:ListItem>Masculino</asp:ListItem>
                                    <asp:ListItem>Feminino</asp:ListItem>
                                    <asp:ListItem>Outro</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSexo" ErrorMessage="Obrigatório" ForeColor="Red" ValidationGroup="Cliente" />
                            </div>
                            <div class="col-md-3">
                                <label for="ddlEstadoCivil">Estado Civil*</label>
                                <asp:DropDownList ID="ddlEstadoCivil" CssClass="form-select" runat="server">
                                    <asp:ListItem>Selecione</asp:ListItem>
                                    <asp:ListItem>Solteiro(a)</asp:ListItem>
                                    <asp:ListItem>Casado(a)</asp:ListItem>
                                    <asp:ListItem>Divorciado(a)</asp:ListItem>
                                    <asp:ListItem>Viúvo(a)</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlEstadoCivil" ErrorMessage="Obrigatório" ForeColor="Red" ValidationGroup="Cliente" />
                            </div>
                        </div>

                        <hr />
                        <h6 class="mb-2 text-secondary">Endereço</h6>

                        <div class="row mb-2">
                            <div class="col-md-2">
                                <label for="txtCep">CEP*</label>
                                <asp:TextBox ID="txtCep" CssClass="form-control" runat="server" placeholder="00000-000"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCep" ErrorMessage="Obrigatório" ForeColor="Red" ValidationGroup="Cliente" />
                            </div>
                            <div class="col-md-4">
                                <label for="txtRua">Rua*</label>
                                <asp:TextBox ID="txtRua" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtRua" ErrorMessage="Obrigatório" ForeColor="Red" ValidationGroup="Cliente" />
                            </div>
                            <div class="col-md-2">
                                <label for="txtNumero">Número*</label>
                                <asp:TextBox ID="txtNumero" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <label for="txtBairro">Bairro*</label>
                                <asp:TextBox ID="txtBairro" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <label for="ddlUf">UF*</label>
                                <asp:DropDownList ID="ddlUf" CssClass="form-select" runat="server">
                                    <asp:ListItem>Selecione</asp:ListItem>
                                    <asp:ListItem>SP</asp:ListItem>
                                    <asp:ListItem>RJ</asp:ListItem>
                                    <asp:ListItem>MG</asp:ListItem>
                                    <asp:ListItem>BA</asp:ListItem>
                                    <asp:ListItem>RS</asp:ListItem>
                                    <asp:ListItem>PR</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnSalvar" runat="server" CssClass="btn btn-success px-4" Text="Salvar" ValidationGroup="Cliente" OnClick="btnSalvar_Click" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
