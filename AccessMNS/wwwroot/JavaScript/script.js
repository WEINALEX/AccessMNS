function saveScrollPosition(chatId) {
    const chatContainer = document.getElementById(chatId);
    if (chatContainer) {
        sessionStorage.setItem("scroll_" + chatId, chatContainer.scrollTop);
    }
}

function restoreScrollPosition(chatId) {
    let chatContainer = document.getElementById(chatId);
    let savedScroll = sessionStorage.getItem("scroll_" + chatId);
    if (chatContainer && savedScroll) {
        chatContainer.scrollTop = savedScroll;
    }
}

function scrollToBottom(chatId) {
    let chatContainer = document.getElementById(chatId);
    if (chatContainer) {
        chatContainer.scrollTop = chatContainer.scrollHeight;
    }
}

function isAtBottom(chatId) {
    let chatContainer = document.getElementById(chatId);
    if (chatContainer) {
        return chatContainer.scrollHeight - chatContainer.scrollTop <= chatContainer.clientHeight + 10;
    }
    return false;
}