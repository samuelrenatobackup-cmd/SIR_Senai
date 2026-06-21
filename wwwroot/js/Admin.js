
    const campoBusca = document.getElementById("campoBusca");
    const contador = document.getElementById("contadorReservas");

    campoBusca.addEventListener("input", function () {

        const termo = this.value.toLowerCase().trim();
        const linhas = document.querySelectorAll(".tabela__linha");

        let visiveis = 0;

        linhas.forEach(linha => {

            const usuario = linha.querySelector(".tabela__usuario span")
                ?.textContent.toLowerCase() || "";

            const equipamento = linha.querySelector(".tabela__equipamento span")
                ?.textContent.toLowerCase() || "";

            const encontrou =
                usuario.includes(termo) ||
                equipamento.includes(termo);

            linha.style.display = encontrou ? "" : "none";

            if (encontrou) {
                visiveis++;
            }
        });

        contador.textContent =
            `Mostrando ${visiveis} reserva(s)`;
    });