window.ethereumInterop = {
    requestAccount: async function () {
        if (typeof window.ethereum !== 'undefined') {
            const accounts = await window.ethereum.request({ method: 'eth_requestAccounts' });
            return accounts[0];
        } else {
            throw new Error('MetaMask is not installed.');
        }
    },
    signMessage: async function (message) {
        const accounts = await window.ethereum.request({ method: 'eth_requestAccounts' });
        const account = accounts[0];
        const signature = await window.ethereum.request({
            method: 'personal_sign',
            params: [message, account],
        });
        return signature;
    }
};
