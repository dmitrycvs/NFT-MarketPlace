// For Timer
// November 30, 2023, at 00:00:00
var date = new Date(Date.UTC(2023, 10, 30, 0, 0, 0)); // UTC date
date.setHours(date.getHours() + 4); // GMT+4

function updateTimer() {
    var now = new Date().getTime();
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

// Hardcoded artist data
const hardcodedArtists = [
    {
        id: "00000000-0000-0000-0000-000000000010",
        name: "Shroomie",
        profileImgPath: "../media/images/avatars/avatar-1.png",
        totalSale: { value: 5, currency: "ETH" },
    },
    {
        id: "00000000-0000-0000-0000-000000000010",
        name: "Arkanio",
        profileImgPath: "../media/images/avatars/avatar-2.png",
        totalSale: { value: 3.23, currency: "ETH" },
    },
    {
        id: "00000000-0000-0000-0000-000000000010",
        name: "Nebulakid",
        profileImgPath: "../media/images/avatars/avatar-3.png",
        totalSale: { value: 4.145, currency: "ETH" },
    },
    {
        id: "00000000-0000-0000-0000-000000000010",
        name: "MoonDancer",
        profileImgPath: "../media/images/avatars/avatar-4.png",
        totalSale: { value: 3.03, currency: "ETH" },
    },
    {
        id: "00000000-0000-0000-0000-000000000010",
        name: "Nebulakid",
        profileImgPath: "../media/images/avatars/avatar-5.png",
        totalSale: { value: 7.85, currency: "ETH" },
    },
    {
        id: "00000000-0000-0000-0000-000000000010",
        name: "Pixelius",
        profileImgPath: "../media/images/avatars/avatar-6.png",
        totalSale: { value: 1.65, currency: "ETH" },
    },


    {
        id: "00000000-0000-0000-0000-000000000010",
        name: "AstroVibe",
        profileImgPath: "../media/images/avatars/avatar-7.png",
        totalSale: { value: 10.1, currency: "ETH" },
    },
    {
        id: "00000000-0000-0000-0000-000000000010",
        name: "Luminova",
        profileImgPath: "../media/images/avatars/avatar-8.png",
        totalSale: { value: 6.5, currency: "ETH" },
    },
    {
        id: "00000000-0000-0000-0000-000000000010",
        name: "Exterfox",
        profileImgPath: "../media/images/avatars/avatar-9.png",
        totalSale: { value: 7.18, currency: "ETH" },
    },
    {
        id: "00000000-0000-0000-0000-000000000010",
        name: "Solarius",
        profileImgPath: "../media/images/avatars/avatar-10.png",
        totalSale: { value: 3.652, currency: "ETH" },
    },

    {
        id: "00000000-0000-0000-0000-000000000010",
        name: "Arcer",
        profileImgPath: "../media/images/avatars/avatar-11.png",
        totalSale: { value: 4.1, currency: "ETH" },
    },
    {
        id: "00000000-0000-0000-0000-000000000010",
        name: "Nebulon",
        profileImgPath: "../media/images/avatars/avatar-12.png",
        totalSale: { value: 11.02, currency: "ETH" },
    },
];

// Populate hardcoded artists
const artistsContainer = document.querySelector(".top-artists__artists");
const loadingElement = document.querySelector(".loader");
loadingElement.style.display = "none";

function populateArtists(artists) {
    artists.forEach((artist) => {
        createArtistBox(artist, artistsContainer);
    });
}

function createArtistBox(artist, artistsContainer) {
    const artistContainer = document.createElement("div");
    artistContainer.classList.add("top-artists__artists__artist");

    const topPart = document.createElement("div");
    topPart.classList.add("top-artists__artists__artist__top");

    const artistAvatar = document.createElement("img");
    artistAvatar.src = artist.profileImgPath;
    artistAvatar.alt = artist.name;

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

    // Bind click event to redirect to hardcoded artist page
    artistContainer.addEventListener("click", () => {
        window.open(`administration/artists/${artist.id}`, "_self");
    });

    artistsContainer.appendChild(artistContainer);
}

// Populate artists with hardcoded data
populateArtists(hardcodedArtists);

document
    .querySelector(".NFT-info__left__artist")
    .addEventListener("click", () => {
        const url = `administration/artists/00000000-0000-0000-0000-000000000010`;
        window.open(url, "_self");
    });
