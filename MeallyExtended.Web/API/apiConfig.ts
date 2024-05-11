let apiBaseUrl: string = '';

if (process.env.NODE_ENV === 'development') {
  apiBaseUrl = 'https://localhost:7279';
} else if (process.env.NODE_ENV === 'production') {
  apiBaseUrl = 'http://localhost:5039';
}

export default apiBaseUrl;