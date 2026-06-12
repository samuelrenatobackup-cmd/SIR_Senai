
function switchTab(target) {
  document.querySelectorAll('.tab-link').forEach(t => t.classList.remove('active'));
  document.querySelectorAll('.form-panel').forEach(p => p.classList.remove('active'));
  document.getElementById('tab-' + target).classList.add('active');
  document.getElementById('panel-' + target).classList.add('active');
}

document.getElementById('tab-login').addEventListener('click', () => switchTab('login'));
document.getElementById('tab-cadastro').addEventListener('click', () => switchTab('cadastro'));
document.getElementById('goto-cadastro').addEventListener('click', e => { e.preventDefault(); switchTab('cadastro'); });
document.getElementById('goto-login').addEventListener('click', e => { e.preventDefault(); switchTab('login'); });
