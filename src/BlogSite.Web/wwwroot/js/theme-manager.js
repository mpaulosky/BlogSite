// Theme Manager for dark/light mode switching
window.ThemeManager = {
    initialize: function() {
        // Check if theme is stored in localStorage, otherwise check system preference
        const storedTheme = localStorage.getItem('theme');
        const isDark = storedTheme === 'dark' || 
                      (!storedTheme && window.matchMedia('(prefers-color-scheme: dark)').matches);
        
        // Apply the theme
        this.applyTheme(isDark);
        
        // Return the current theme state
        return isDark;
    },
    
    toggleTheme: function(isDark) {
        this.applyTheme(isDark);
        localStorage.setItem('theme', isDark ? 'dark' : 'light');
    },
    
    applyTheme: function(isDark) {
        if (isDark) {
            document.documentElement.classList.add('dark');
        } else {
            document.documentElement.classList.remove('dark');
        }
    }
};

// Initialize theme on page load to avoid FOUC (Flash of Unstyled Content)
(function() {
    const storedTheme = localStorage.getItem('theme');
    const isDark = storedTheme === 'dark' || 
                  (!storedTheme && window.matchMedia('(prefers-color-scheme: dark)').matches);
    
    if (isDark) {
        document.documentElement.classList.add('dark');
    }
})();
