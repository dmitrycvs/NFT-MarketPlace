window.ethereumInterop = {
    requestAccount: async function () {
        try {
            if (typeof window.ethereum !== 'undefined') {
                const accounts = await window.ethereum.request({ method: 'eth_requestAccounts' });
                if (accounts && accounts.length > 0) {
                    return accounts[0];
                } else {
                    throw new Error('No account found.');
                }
            } else {
                this.showMetaMaskModal();
                throw new Error('MetaMask is not installed.');
            }
        } catch (error) {
            console.error('Error in requestAccount:', error);
            this.showMetaMaskModal();
            throw error;
        }
    },

    signMessage: async function (message) {
        try {
            // Request the account before signing the message
            const account = await this.requestAccount();
            const signature = await window.ethereum.request({
                method: 'personal_sign',
                params: [message, account],
            });
            return signature;
        } catch (error) {
            console.error('Error in signMessage:', error);
            throw new Error('Error signing message: ' + error.message);
        }
    },

    isMetaMaskInstalled: function () {
        // Checks if MetaMask is installed and available
        return typeof window.ethereum !== 'undefined';
    },

    showMetaMaskModal: function () {
        const modal = document.getElementById('metamask-modal');
        if (modal) {
            modal.classList.remove('hidden');
        }
    },

    hideMetaMaskModal: function () {
        const modal = document.getElementById('metamask-modal');
        if (modal) {
            modal.classList.add('hidden');
        }
    }
};

document.getElementById('close-modal')?.addEventListener('click', () => {
    window.ethereumInterop.hideMetaMaskModal();
});
