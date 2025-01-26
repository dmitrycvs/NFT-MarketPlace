//For tab buttons:
const todayBtn = document.getElementById("today-button");
const thisWeekBtn = document.getElementById("this-week-button");
const thisMonthBtn = document.getElementById("this-month-button");
const allTimeBtn = document.getElementById("all-time-button");

function btnClick(button) {
  todayBtn.classList.remove("clicked-on");
  thisWeekBtn.classList.remove("clicked-on");
  thisMonthBtn.classList.remove("clicked-on");
  allTimeBtn.classList.remove("clicked-on");
  button.classList.add("clicked-on");
}

todayBtn.addEventListener("click", () => {
  btnClick(todayBtn);
});
thisWeekBtn.addEventListener("click", () => {
  btnClick(thisWeekBtn);
});
thisMonthBtn.addEventListener("click", () => {
  btnClick(thisMonthBtn);
});
allTimeBtn.addEventListener("click", () => {
  btnClick(allTimeBtn);
});

btnClick(todayBtn);

//API part:
const API_BASE_URL = "https://nft-marketplace-6ncs.onrender.com/api/creators";
const artistsContainer = document.querySelector(".artists__container");
const loadingElement = document.querySelector(".artists__container__loader");
loadingElement.style.display = "none";
let artists;

function getData() {
  loadingElement.style.display = "flex";
  fetch(API_BASE_URL)
    .then((res) => {
      if (res.status !== 200) {
      }
      return res.json();
    })
    .then((data) => {
      artists = data;
      sortArtists(artists, "id", "asc", true);
      artists.forEach((artist, idx) => {
        createArtistBox(artist, artistsContainer);
        newArtist = document.querySelectorAll(".artists__container__artist")[
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
      document.querySelector(".artists__container").innerHTML = errorMessage;
      document.querySelector(".artists__btns").style.display = "none";
    })
    .finally(() => {
      loadingElement.style.display = "none";
    });
}

getData();

function createArtistBox(artist, artistsContainer) {
  const artistContainer = document.createElement("div");
  artistContainer.classList.add("artists__container__artist");

  artistContainer.innerHTML = `<div class="artists__container__artist__rank-artist">
  <div class="artists__container__artist__rank-artist__rank">${artist.id}</div>
  <div class="artists__container__artist__rank-artist__artist">
    <img
      src="../../../${artist.profileImgPath}"
      alt="${artist.name} avatar"
    />
    <h5>${artist.name}</h5>
  </div>
</div>
<div class="artists__container__artist__stats">
  <p>+${artist.totalSale.value}%</p>
  <p>${artist.nftSold}</p>
  <p>${artist.volume.substring(0, artist.volume.length - 3)}k</p>
  <div class="delete-btn">
    <img
      src="media/icons/trash-bin.svg"
      alt="trash bin icon"
    />
  </div>
</div>`;

  const artistDeleteBtn =
    artistContainer.getElementsByClassName("delete-btn")[0];
  artistDeleteBtn.addEventListener("click", (e) => {
    artistDelete(artist.id, artist.name, artistContainer);
    e.stopPropagation();
  });

  artistsContainer.appendChild(artistContainer);
}

async function artistDelete(artistId, artistName, artistContainer) {
  if (
    confirm(`Are you sure you want to delete ${artistName} from the Rankings?
Keep in mind that deleting the artist also removes their NFts from the Marketplace.`)
  ) {
    const response = await fetch(`${API_BASE_URL}/${artistId}`, {
      method: "DELETE",
    });
    if (response.status === 200) {
      artistContainer.remove();
      removeArtistFromLocalStorage(artistName);
    }
  }
}

function removeArtistFromLocalStorage(artistName) {
  const favoriteNfts = JSON.parse(localStorage.getItem("favoriteNfts")) || [];
  const updatedFavorites = favoriteNfts.filter(
    (artist) => artist[0] !== artistName
  );
  localStorage.setItem("favoriteNfts", JSON.stringify(updatedFavorites));
}

//sorting buttons
const idSort = document.getElementById("id-sort");
const nameSort = document.getElementById("name-sort");
const changeSort = document.getElementById("change-sort");
const nftSort = document.getElementById("nft-sort");
const volumeSort = document.getElementById("volume-sort");

function sortArtists(property, sortOrder) {
  //first we select our artists inside the container witout selecting the loader
  const artistsArray = Array.from(artistsContainer.children).filter(
    (child) => !child.classList.contains("artists__container__loader")
  );

  artistsArray.sort((a, b) => {
    const valueA = getSortValue(a, property);
    const valueB = getSortValue(b, property);

    if (Number.isNaN(valueA) && Number.isNaN(valueB)) {
      if (sortOrder == "ascending") {
        return valueA - valueB;
      } else {
        return valueB - valueA;
      }
    } else {
      if (sortOrder == "ascending") {
        //this one is for sorting the string
        return valueA < valueB ? -1 : valueA > valueB ? 1 : 0;
      } else {
        return valueA > valueB ? -1 : valueA < valueB ? 1 : 0;
      }
    }
  });

  artistsContainer.innerHTML = "";
  artistsArray.forEach((artist) => {
    artistsContainer.appendChild(artist);
  });
}

//For getting and converting the values of artists in order to sort them:
function getSortValue(element, property) {
  switch (property) {
    case "id":
      return parseInt(
        element.querySelector(".artists__container__artist__rank-artist__rank")
          .textContent
      );
    case "name":
      return element.querySelector(
        ".artists__container__artist__rank-artist__artist h5"
      ).textContent;
    case "change":
      return parseFloat(
        element
          .querySelector(".artists__container__artist__stats p:nth-child(1)")
          .textContent.slice(1, -1)
      );
    case "nftSold":
      return parseInt(
        element.querySelector(
          ".artists__container__artist__stats p:nth-child(2)"
        ).textContent
      );
    case "volume":
      return parseInt(
        element.querySelector(
          ".artists__container__artist__stats p:nth-child(3)"
        ).textContent
      );
  }
}

let currentSort = {
  //since the API returns the artists in sorted form by id, this will be the current sorting order
  property: "id",
  order: "ascending",
};

idSort.addEventListener("click", () => sortAndRenderArtists("id"));
nameSort.addEventListener("click", () => sortAndRenderArtists("name"));
changeSort.addEventListener("click", () => sortAndRenderArtists("change"));
nftSort.addEventListener("click", () => sortAndRenderArtists("nftSold"));
volumeSort.addEventListener("click", () => sortAndRenderArtists("volume"));

function sortAndRenderArtists(property) {
  if (currentSort.property === property) {
    currentSort.order =
      currentSort.order === "ascending" ? "descending" : "ascending";
  } else {
    //if it's a different property, we set order to ascending
    currentSort.order = "ascending";
  }
  currentSort.property = property;
  sortArtists(property, currentSort.order);
}
