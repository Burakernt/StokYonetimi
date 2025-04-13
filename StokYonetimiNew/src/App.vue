<template>
    <div class="container-fluid">
        <div class="row">
            <!-- Sidebar -->
            <div class="col-md-3 col-lg-2 p-0 bg-dark sidebar">
                <div class="d-flex flex-column flex-shrink-0 p-3 text-white bg-dark vh-100">
                    <a href="#/" class="d-flex align-items-center mb-3 mb-md-0 me-md-auto text-white text-decoration-none">
                        <span class="fs-4">Envanter Yönetimi</span>
                    </a>
                    <hr>
                    <ul class="nav nav-pills flex-column mb-auto">
                        <li class="nav-item">
                            <a href="#/" class="nav-link text-white" :class="{ active: currentPage === 'home' }">
                                <i class="mdi mdi-view-dashboard me-2"></i>
                                Ana Sayfa
                            </a>
                        </li>
                        <li>
                            <a href="#/supplier" class="nav-link text-white" :class="{ active: currentPage === 'supplier' }">
                                <i class="mdi mdi-truck me-2"></i>
                                Tedarikçi Tanýmlarý
                            </a>
                        </li>
                        <li>
                            <a href="#/material-group" class="nav-link text-white" :class="{ active: currentPage === 'materialGroup' }">
                                <i class="mdi mdi-shape me-2"></i>
                                Malzeme Grup Tanýmlarý
                            </a>
                        </li>
                        <li>
                            <a href="#/material" class="nav-link text-white" :class="{ active: currentPage === 'material' }">
                                <i class="mdi mdi-package-variant me-2"></i>
                                Malzeme/Ürün Tanýmlarý
                            </a>
                        </li>
                        <li>
                            <a href="#/customer-team" class="nav-link text-white" :class="{ active: currentPage === 'customerTeam' }">
                                <i class="mdi mdi-account-group me-2"></i>
                                Müþteri/Ekip Tanýmlarý
                            </a>
                        </li>
                    </ul>
                </div>
            </div>

            <!-- Main content -->
            <div class="col-md-9 col-lg-10 ms-sm-auto p-4">
                <main>
                    <component :is="currentView"></component>
                </main>
            </div>
        </div>
    </div>
</template>

<script>
    import Home from './views/Home.vue';
    import Supplier from './views/Supplier.vue';
    import MaterialGroup from './views/MaterialGroup.vue';
    import Material from './views/Material.vue';
    import CustomerTeam from './views/CustomerTeam.vue';

    export default {
        components: {
            Home,
            Supplier,
            MaterialGroup,
            Material,
            CustomerTeam
        },
        data() {
            return {
                currentPage: 'home',
                routes: {
                    '': 'home',
                    '#/': 'home',
                    '#/supplier': 'supplier',
                    '#/material-group': 'materialGroup',
                    '#/material': 'material',
                    '#/customer-team': 'customerTeam'
                }
            };
        },
        computed: {
            currentView() {
                return this.currentPage.charAt(0).toUpperCase() + this.currentPage.slice(1);
            }
        },
        methods: {
            hashChanged() {
                const hash = window.location.hash;
                this.currentPage = this.routes[hash] || 'home';
            }
        },
        mounted() {
            this.hashChanged();
            window.addEventListener('hashchange', this.hashChanged);
        },
        beforeUnmount() {
            window.removeEventListener('hashchange', this.hashChanged);
        }
    }
</script>

<style>
    /* Stil tanýmlarý style.css dosyasýna taþýndý */
</style>
