
            const searchInput = document.getElementById("search");
            const filterButtons = document.querySelectorAll(".filter-btn");
            const cards = document.querySelectorAll(".card");
            const semResultados = document.getElementById("semResultados");

            let filtroAtual = "todos";

            function filtrarEquipamentos() {

                const busca = searchInput.value.toLowerCase();
                let quantidadeVisivel = 0;

                cards.forEach(card => {

                    const nome = card.querySelector("h3").textContent.toLowerCase();
                    const status = card.dataset.status;

                    const correspondeBusca =
                        nome.includes(busca);

                    const correspondeFiltro =
                        filtroAtual === "todos" ||
                        status === filtroAtual;

                    if (correspondeBusca && correspondeFiltro) {
                        card.style.display = "";
                        quantidadeVisivel++;
                    }
                    else {
                        card.style.display = "none";
                    }

                });

                if (quantidadeVisivel === 0) {
                    semResultados.style.display = "block";
                } else {
                    semResultados.style.display = "none";
                }
            }

            searchInput.addEventListener("input", filtrarEquipamentos);

            filterButtons.forEach(btn => {

                btn.addEventListener("click", () => {

                    filterButtons.forEach(b =>
                        b.classList.remove("active")
                    );

                    btn.classList.add("active");

                    filtroAtual = btn.dataset.filter;

                    filtrarEquipamentos();

                });

            });
