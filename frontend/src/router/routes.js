
const routes = [
  {
    // Routes go here,
    // Define routes by adding an object in this array with path and component properties.

    path: '/',
    component: () => import('layouts/MainLayout.vue'),
    children: [
      { path: '', component: () => import('pages/IndexPage.vue') },
      {
        path: '/home',
        component: () => import('../pages/HomePage.vue')
      },
      {
        path: '/dashboard',
        component: () => import('../pages/DashboardPage.vue')
      }

    ]
  },

  // Always leave this as last one,
  // but you can also remove it
  {
    path: '/:catchAll(.*)*',
    component: () => import('pages/ErrorNotFound.vue')
  }
]

export default routes
