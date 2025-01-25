//For Timer
//November 30, 2023, at 00:00:00
var date = new Date(Date.UTC(2023, 10, 30, 0, 0, 0)); // UTC дата
date.setHours(date.getHours() + 4); // GMT+4

function updateTimer() {
    var now = new Date().getTime()
    var remainingTime = date.getTime() - now;

    if (remainingTime <= 0) {
        document.getElementById("hours").textContent = "23";
        document.getElementById("minutes").textContent = "12";
        document.getElementById("seconds").textContent = "01";
        return;
    }

    var hours = Math.floor(remainingTime / (1000 * 60 * 60));
    var minutes = Math.floor((remainingTime % (1000 * 60 * 60)) / (1000 * 60));
    var seconds = Math.floor((remainingTime % (1000 * 60)) / 1000);

    document.getElementById("hours").textContent =
        hours < 10 ? "0" + hours : hours;
    document.getElementById("minutes").textContent =
        minutes < 10 ? "0" + minutes : minutes;
    document.getElementById("seconds").textContent =
        seconds < 10 ? "0" + seconds : seconds;
}

document.addEventListener("DOMContentLoaded", function () {
    setInterval(updateTimer, 1000);
});

//API part:

const API_BASE_URL = "https://nft-marketplace-6ncs.onrender.com/api/creators";
const artistsContainer = document.querySelector(".top-artists__artists");
const loadingElement = document.querySelector(".loader");
loadingElement.style.display = "none";

function getData() {
    loadingElement.style.display = "grid";
    fetch(API_BASE_URL)
        .then((res) => {
            if (res.status !== 200) {
            }
            return res.json();
        })
        .then((data) => {
            let artists = data;
            artists.forEach((artist, idx) => {
                createArtistBox(artist, artistsContainer);
                newArtist = document.querySelectorAll(".top-artists__artists__artist")[
                    idx
                ];
                newArtist.addEventListener("click", () => {
                    window.open(
                        `../../../client/pages/artist/index.html?artist_id=${artist.id}`,
                        "_self"
                    );
                });
            });
        })
        .catch((err) => {
            const errorMessage = `
      <div id="bad-request">
      <img src="../../media/icons/sad-face.svg" alt="sad face icon" />
        <p>Sorry, we are experiencing technical difficulties with our API server. Please check back later.</p>
      </div>`;
            document.querySelector(".top-artists__artists").innerHTML = errorMessage;
            document.querySelector(".top-artists__mobile-btn").style.display = "none";
            document.querySelector(".top-artists__header a").style.display = "none";
            document.querySelector(".top-artists__artists").style.display = "initial";
        })
        .finally(() => {
            loadingElement.style.display = "none";
        });
}

getData();

function createArtistBox(artist, artistsContainer) {
    const artistContainer = document.createElement("div");
    artistContainer.classList.add("top-artists__artists__artist");

    const topPart = document.createElement("div");
    topPart.classList.add("top-artists__artists__artist__top");

    const artistId = document.createElement("div");
    artistId.textContent = artist.id;

    const artistAvatar = document.createElement("img");
    artistAvatar.src = `../../media/${artist.profileImgPath}`;
    artistAvatar.alt = artist.name;

    topPart.appendChild(artistId);
    topPart.appendChild(artistAvatar);

    const bottomPart = document.createElement("div");
    bottomPart.classList.add("top-artists__artists__artist__bottom");

    const artistName = document.createElement("h5");
    artistName.textContent = artist.name;

    const totalSales = document.createElement("div");
    totalSales.innerHTML = `<p>Total Sales:</p>
    <p class="top-artists__artists__artist__bottom__sales">${artist.totalSale.value} ${artist.totalSale.currency}</p>`;

    bottomPart.appendChild(artistName);
    bottomPart.appendChild(totalSales);

    artistContainer.appendChild(topPart);
    artistContainer.appendChild(bottomPart);

    artistsContainer.appendChild(artistContainer);
}

document
    .querySelector(".NFT-info__left__artist")
    .addEventListener("click", () => {
        const artistId = 7;
        const url = `../../../client/pages/artist/index.html?artist_id=${artistId}`;
        window.open(url, "_self");
    });
