
### What's New?

We are excited to announce several important updates for the Big Data and NFT tracking application.

### Key Updates:

- Big Data Integration: We've integrated Hadoop as the new backend storage solution, significantly improving scalability and performance for handling big data workloads.

- Cloud Deployment: The application has been fully migrated to AWS cloud infrastructure, enhancing flexibility and resource management.

- NFT Data Tracking: We’ve implemented the OpenSea API to fetch and display detailed NFT data such as sales volume, average price, and number of owners, providing deeper insights into the market.

- Hyperparameter Optimization: We’ve refined our machine learning models with hyperparameter optimization techniques like grid search and random search to improve prediction accuracy for NFT price trends.

### Why the Change?

*These updates are designed to:*

- Improve Performance and Scalability: By integrating Hadoop, we can handle larger datasets more efficiently, making the system more scalable for big data tasks.

- Enhance Data Access: Cloud deployment allows for faster data retrieval and processing, improving overall system responsiveness.

- Increase Market Insights: By integrating the OpenSea API, we can provide up-to-date NFT data directly within the application, offering users richer insights into the NFT market.

- Error Handling: With improved error handling in place, the application will now handle issues like missing or malformed data more gracefully, ensuring a smoother user experience.


#### Changes for October 7 - October 19, 2024

- Big Data Integration: We have successfully integrated Hadoop as the new backend storage solution. This update improves the scalability and performance of data processing tasks, making it easier to manage and analyze big data workloads.

- Cloud Deployment: The application has been fully migrated to AWS cloud infrastructure.

- Documentation on EIP-1155: EIP-1155 is a multi-token standard that can be useful for NFT projects. Read the EIP-1155 documentation to understand how to leverage this standard for enhancing functionality and optimizing storage. 

- Setup: Obtained the necessary API key after overcoming initial registration challenges, with support from teammates.

- Integrated the OpenSea API into the ASP.NET Core application using RestSharp to fetch NFT data.

- Implemented error handling for invalid or missing data to ensure the application handled API failures gracefully.

- Formatted and displayed the response data in a user-friendly format using Razor Pages, showcasing key metrics like sales volume, average price, and number of owners.


#### Changes for October 23 - November 7, 2024

- Hyperparameter Optimization: Using grid search and random search for hyperparameter tuning in situations where a more exhaustive search was needed.

- Analysis of the NFT Market on Different Networks: Read articles about the characteristics of NFTs on different blockchains (Polygon, BNB Chain, Arbitrum, etc.) to understand their pros and cons. 

- Article on Smart Contract Security: Security is crucial in smart contract development. Go through Consensys: Smart Contract Security Best Practices to better understand how to protect your contracts from potential vulnerabilities.

- Implemented error handling for API response issues, including missing data and malformed JSON, through validation and testing.

- Secured the API key by storing it in the appSettings.json file after resolving initial configuration challenges.

- Applied asynchronous programming to ensure smooth data fetching and UI updates, keeping the application responsive while waiting for API data.