// Tab Buttons
const createdBtn = document.getElementById("created-button");
const ownedBtn = document.getElementById("owned-button");
const collectionBtn = document.getElementById("collection-button");
const nftCardsContainer = document.querySelector(".nft-cards .container");
const collectionContainer = document.querySelector(".nft-cards__collection");
const loaderContainer = document.querySelector(".loader-container");

function btnClick(button) {
    createdBtn.classList.remove("clicked-on");
    ownedBtn.classList.remove("clicked-on");
    collectionBtn.classList.remove("clicked-on");
    button.classList.add("clicked-on");
}

collectionBtn.addEventListener("click", () => {
    btnClick(collectionBtn);
    nftCardsContainer.style.display = "none";
    collectionContainer.innerHTML = "";
    populateCollection();
    if (collectionContainer.childElementCount > 0) {
        collectionContainer.style.display = "grid";
        document.querySelector(".nft-cards__empty").style.display = "none";
    } else {
        document.querySelector(".nft-cards__empty").style.display = "initial";
    }
});

createdBtn.addEventListener("click", () => {
    btnClick(createdBtn);
    collectionContainer.style.display = "none";
    if (nftCardsContainer.childElementCount > 0) {
        nftCardsContainer.style.display = "grid";
        document.querySelector(".nft-cards__empty").style.display = "none";
    } else {
        document.querySelector(".nft-cards__empty").style.display = "initial";
    }
});

ownedBtn.addEventListener("click", () => {
    btnClick(ownedBtn);
    nftCardsContainer.style.display = "none";
    collectionContainer.style.display = "none";
    document.querySelector(".nft-cards__empty").style.display = "initial";
});

btnClick(createdBtn);

// Hardcoded Artist Data
const hardcodedArtist = {
    id: "00000000-0000-0000-0000-000000000010",
    name: "Hardcoded Artist Name",
    profileImgPath: "../media/images/avatars/avatar-10.png",
    bio: "This is a hardcoded artist bio.",
    volume: "50000",
    nftSold: "100",
    followers: "10000",
    nfts: [
        {
            name: "Hardcoded NFT 1",
            imgPath: "../media/images/nft-1.png",
            price: { value: 1.5, currency: "ETH" },
            highestBid: { value: 1.2, currency: "ETH" },
        },
        {
            name: "Hardcoded NFT 2",
            imgPath: "../media/images/nft-2.png",
            price: { value: 2.0, currency: "ETH" },
            highestBid: { value: 1.8, currency: "ETH" },
        },
    ],
};

function fillArtistPage(artist) {
    document.querySelector(".artist__left h2").textContent = artist.name;
    document.querySelector(".artist__left img").src = artist.profileImgPath;
    document.querySelector(".artist__left__bio p").textContent = artist.bio;
    const formattedVolume = formatNumberInThousands(artist.volume);
    document.querySelector(
        ".artist__left__stats div:nth-child(1) h4"
    ).textContent = `${formattedVolume}+`;
    document.querySelector(
        ".artist__left__stats div:nth-child(2) h4"
    ).textContent = `${artist.nftSold}+`;
    document.querySelector(
        ".artist__left__stats div:nth-child(3) h4"
    ).textContent = `${artist.followers}+`;

    // NFT Cards
    const nftCount = artist.nfts.length;
    document.querySelector("#created-button div").innerHTML = nftCount;
    if (nftCount === 0) {
        nftCardsContainer.style.display = "none";
        document.querySelector(".nft-cards__empty").style.display = "initial";
    } else {
        artist.nfts.forEach((nft) => {
            nftCardsContainer.appendChild(createNftCard(artist, nft));
        });
    }

    // Hide the loader after loading data
    loaderContainer.style.display = "none";
}

// Create NFT Card
function createNftCard(artist, nft) {
    const nftCard = document.createElement("div");
    nftCard.classList.add("nft-cards__card");
    nftCard.innerHTML = `
    <div id="heart-icon">
      <img src="media/icons/heart.svg" alt="heart icon" />
    </div>
    <img src=${nft.imgPath} alt="${nft.name} NFT" />
    <div class="nft-cards__card__text">
      <h5>${nft.name}</h5>
      <div class="nft-cards__card__text__artist">
        <img src="${artist.profileImgPath}" alt="${artist.name}" />
        <span>${artist.name}</span>
      </div>
      <div class="nft-cards__card__text__details">
        <div class="nft-cards__card__text__details__price">
          <h5>Price</h5>
          <p>${nft.price.value} ${nft.price.currency}</p>
        </div>
        <div class="nft-cards__card__text__details__bid">
          <h5>Highest Bid</h5>
          <p>${nft.highestBid.value} ${nft.highestBid.currency}</p>
        </div>
      </div>
    </div>
  `;
    return nftCard;
}

// Format Number in Thousands
function formatNumberInThousands(numberStr) {
    const number = parseInt(numberStr);
    if (!isNaN(number)) {
        if (number >= 1000) {
            return (number / 1000).toFixed(0) + "k";
        }
        return number.toString();
    }
    return numberStr;
}

// Populate Hardcoded Data
fillArtistPage(hardcodedArtist);
