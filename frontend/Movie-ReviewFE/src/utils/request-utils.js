
// generic fetch method

const fetchData = async (url) => {


  try {
      const response = await fetch(url);

      if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
      }

      // Try to parse JSON response, but handle errors if the response isn't valid JSON
      const data = await response.json();
      return data;
  } catch (error) {
      console.error('Fetch error:', error.message);
      throw error; // Optionally rethrow to handle upstream
  }
};

export default fetchData;