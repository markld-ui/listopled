export default defineNuxtConfig({
  srcDir: 'src/',
  css: ['~/assets/styles/main.css'],
  app: {
    head: {
      htmlAttrs: {
        lang: 'ru'
      },
      title: 'ЛИСТОПЛЕД',
      meta: [
        {
          name: 'description',
          content: 'Каркас сайта ЛИСТОПЛЕД. Фаза 1.'
        }
      ]
    }
  },
  runtimeConfig: {
    internalApiBaseUrl: process.env.INTERNAL_API_BASE_URL || 'http://api:8080/api/v1',
    public: {
      siteUrl: process.env.NUXT_PUBLIC_SITE_URL || 'http://localhost:3000'
    }
  }
})
