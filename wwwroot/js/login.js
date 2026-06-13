function switchTab(tab) {
  const abaLogin = document.getElementById('aba-login');
  const abaCadastro = document.getElementById('aba-cadastro');
  const painelLogin = document.getElementById('painel-login');
  const painelCadastro = document.getElementById('painel-cadastro');

  if (tab === 'cadastro') {
    abaLogin.classList.remove('active');
    abaCadastro.classList.add('active');

    painelLogin.classList.remove('active');
    painelCadastro.classList.add('active');
  } else {
    abaCadastro.classList.remove('active');
    abaLogin.classList.add('active');

    painelCadastro.classList.remove('active');
    painelLogin.classList.add('active');
  }
}

document.getElementById('aba-login').addEventListener('click', () => switchTab('login'));
document.getElementById('aba-cadastro').addEventListener('click', () => switchTab('cadastro'));

document.getElementById('ir-cadastro').addEventListener('click', e => {
  e.preventDefault();
  switchTab('cadastro');
});

document.getElementById('ir-login').addEventListener('click', e => {
  e.preventDefault();
  switchTab('login');
});