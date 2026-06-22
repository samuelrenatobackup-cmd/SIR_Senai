console.log("login.js carregou");
const abaInicial = window.__abaInicial || 'login';
if (abaInicial === 'cadastro') {
  document.getElementById('aba-cadastro').classList.add('active');
  document.getElementById('aba-login').classList.remove('active');
  document.getElementById('painel-cadastro').classList.add('active');
  document.getElementById('painel-login').classList.remove('active');
}
document.addEventListener("DOMContentLoaded", function () {
    function switchTab(tab) {

        const abaLogin = document.getElementById('aba-login');
        const abaCadastro = document.getElementById('aba-cadastro');
        const painelLogin = document.getElementById('painel-login');
        const painelCadastro = document.getElementById('painel-cadastro');


        if(tab === "cadastro") {

            abaLogin?.classList.remove('active');
            abaCadastro?.classList.add('active');

            painelLogin?.classList.remove('active');
            painelCadastro?.classList.add('active');

        } else {

            abaCadastro?.classList.remove('active');
            abaLogin?.classList.add('active');

            painelCadastro?.classList.remove('active');
            painelLogin?.classList.add('active');

        }

    }
    document.getElementById('aba-login')
        ?.addEventListener('click', () => switchTab('login'));
    document.getElementById('aba-cadastro')
        ?.addEventListener('click', () => switchTab('cadastro'));
    document.getElementById('ir-cadastro')
        ?.addEventListener('click', e => {

            e.preventDefault();
            switchTab('cadastro');

        });
    document.getElementById('ir-login')
        ?.addEventListener('click', e => {

            e.preventDefault();
            switchTab('login');

        });
    // LOGIN MOSTRAR SENHA

    const mostrarLogin = document.getElementById('mostrar-senha-login');

    mostrarLogin?.addEventListener('change', function(){

        const senha = document.getElementById('login-senha');

        senha.type = this.checked ? "text" : "password";

    });
    // CADASTRO MOSTRAR SENHA

    const mostrarCadastro = document.getElementById('mostrar-senha-cadastro');


    mostrarCadastro?.addEventListener('change', function(){

        const senha = document.getElementById('cadastro-senha');
        const confirmar = document.getElementById('cadastro-confirmar');


        const tipo = this.checked ? "text" : "password";


        senha.type = tipo;
        // confirmar.type = tipo;
    });
});