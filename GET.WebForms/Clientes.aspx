<%@ Page Title="Clientes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="GET.WebForms.Clientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Clientes</h2>
    
    <div class="row">
        <div class="col-md-12">
            <table id="clientesTable" class="table table-striped">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Nome</th>
                        <th>Email</th>
                        <th>Ações</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            carregarClientes();
        });

        function carregarClientes() {
            $.ajax({
                url: 'http://localhost:PORT/api/clientes',
                method: 'GET',
                success: function (data) {
                    var tbody = $('#clientesTable tbody');
                    tbody.empty();

                    data.forEach(function (cliente) {
                        tbody.append(`
                            <tr>
                                <td>${cliente.Id}</td>
                                <td>${cliente.Nome}</td>
                                <td>${cliente.Email}</td>
                                <td>
                                    <button onclick="editarCliente(${cliente.Id})" class="btn btn-sm btn-primary">Editar</button>
                                    <button onclick="excluirCliente(${cliente.Id})" class="btn btn-sm btn-danger">Excluir</button>
                                </td>
                            </tr>
                        `);
                    });
                },
                error: function (xhr, status, error) {
                    alert('Erro ao carregar clientes: ' + error);
                }
            });
        }

        function excluirCliente(id) {
            if (!confirm('Deseja realmente excluir este cliente?'))
                return;

            $.ajax({
                url: `http://localhost:PORT/api/clientes/${id}`,
                method: 'DELETE',
                success: function () {
                    alert('Cliente excluído com sucesso!');
                    carregarClientes();
                },
                error: function (xhr, status, error) {
                    alert('Erro ao excluir cliente: ' + error);
                }
            });
        }

        // Adicione outras funções conforme necessário (editarCliente, salvarCliente, etc)
    </script>
</asp:Content>