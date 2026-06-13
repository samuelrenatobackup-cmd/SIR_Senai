document.addEventListener("DOMContentLoaded", () => {

    const searchInput = document.getElementById("search");
    const filterButtons = document.querySelectorAll(".botao-filtro");
    const cards = document.querySelectorAll(".card");
    const semResultados = document.getElementById("semResultados");

    console.log("Reserva.js carregado");
    console.log("Cards encontrados:", cards.length);
    console.log("Botões de filtro encontrados:", filterButtons.length);

    let filtroAtual = "todos";

    function filtrarEquipamentos() {
        const busca = searchInput.value.toLowerCase();
        let quantidadeVisivel = 0;

        cards.forEach(card => {
            const h3 = card.querySelector("h3");
            const nome = h3 ? h3.textContent.toLowerCase() : "";
            const status = card.dataset.status;

            const correspondeBusca = nome.includes(busca);
            const correspondeFiltro =
                filtroAtual === "todos" || status === filtroAtual;

            if (correspondeBusca && correspondeFiltro) {
                card.style.display = "";
                quantidadeVisivel++;
            } else {
                card.style.display = "none";
            }
        });

        if (semResultados) {
            semResultados.style.display =
                quantidadeVisivel === 0 ? "block" : "none";
        }
    }

    if (searchInput) {
        searchInput.addEventListener("input", filtrarEquipamentos);
    }

    filterButtons.forEach(btn => {
        btn.addEventListener("click", () => {
            console.log("Clicou no filtro:", btn.dataset.filter);

            filterButtons.forEach(b => b.classList.remove("ativo"));
            btn.classList.add("ativo");

            filtroAtual = btn.dataset.filter;
            filtrarEquipamentos();
        });
    });

});