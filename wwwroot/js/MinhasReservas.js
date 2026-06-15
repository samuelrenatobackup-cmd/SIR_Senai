 function abrirModal(reservaId, nomeEquipamento, codigo) {
            document.getElementById('modalReservaId').value = reservaId;
            document.getElementById('modalNomeEquipamento').textContent = nomeEquipamento;
            document.getElementById('modalOverlay').classList.add('visivel');
        }

        function fecharModal() {
            document.getElementById('modalOverlay').classList.remove('visivel');
        }

        // Fecha o modal ao clicar fora dele
        document.getElementById('modalOverlay').addEventListener('click', function (e) {
            if (e.target === this) fecharModal();
        });

        // Fecha com Escape
        document.addEventListener('keydown', function (e) {
            if (e.key === 'Escape') fecharModal();
        });