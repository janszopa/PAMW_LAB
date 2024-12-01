 // Funkcja do wczytywania cytatów z Local Storage
 function getQuotes() {
    let quotes = localStorage.getItem('quotes');
    return quotes ? JSON.parse(quotes) : [];
}

// Funkcja do zapisywania cytatów do Local Storage
function saveQuotes(quotes) {
    localStorage.setItem('quotes', JSON.stringify(quotes));
}

// Funkcja do aktualizacji widoku (wyświetlanie cytatów)
function renderQuotes() {
    const quoteList = document.getElementById('quote-list');
    quoteList.innerHTML = ''; // Wyczyszczenie listy
    const quotes = getQuotes();

    quotes.forEach((quote, index) => {
        const li = document.createElement('li');
        li.textContent = quote;

        // Przycisk do usuwania cytatu
        const deleteButton = document.createElement('button');
        deleteButton.textContent = 'Delete';
        deleteButton.addEventListener('click', () => deleteQuote(index));

        // Przycisk do edycji cytatu
        const editButton = document.createElement('button');
        editButton.textContent = 'Edit';
        editButton.addEventListener('click', () => editQuote(index, quote));

        li.appendChild(editButton);
        li.appendChild(deleteButton);
        quoteList.appendChild(li);
    });
}

// Funkcja do dodawania nowego cytatu
function addQuote() {
    const newQuoteInput = document.getElementById('new-quote');
    const newQuote = newQuoteInput.value.trim();

    if (newQuote) {
        const quotes = getQuotes();
        quotes.push(newQuote);
        saveQuotes(quotes);
        newQuoteInput.value = ''; // Wyczyszczenie pola tekstowego
        renderQuotes();
    }
}

// Funkcja do usuwania cytatu
function deleteQuote(index) {
    const quotes = getQuotes();
    quotes.splice(index, 1);
    saveQuotes(quotes);
    renderQuotes();
}

// Funkcja do edycji cytatu
function editQuote(index, quote) {
    const newQuote = prompt('Edit your quote', quote);
    if (newQuote !== null && newQuote.trim() !== '') {
        const quotes = getQuotes();
        quotes[index] = newQuote.trim();
        saveQuotes(quotes);
        renderQuotes();
    }
}

// Podłączanie event listenera do przycisku dodawania cytatu
document.getElementById('add-quote-btn').addEventListener('click', addQuote);

// Wczytanie cytatów po załadowaniu strony
renderQuotes();