// Wait for the DOM to load
document.addEventListener('DOMContentLoaded', function() {
    // Initialize Vue app
    const { createApp, ref, computed, onMounted } = Vue;
    
    createApp({
        setup() {
            // Data
            const suppliers = ref([]);
            const loading = ref(true);
            const isSubmitting = ref(false);
            const searchQuery = ref('');
            const errors = ref({});
            const similarSuppliers = ref([]);
            const activeSearchField = ref(null);
            
            // Modal states
            const showCreateModal = ref(false);
            const showEditModal = ref(false);
            const showDeleteModal = ref(false);
            
            // Form data
            const newSupplier = ref({
                shortName: '',
                fullName: '',
                taxNumber: '',
                district: '',
                city: '',
                fullAddress: '',
                landlinePhone: '',
                mobilePhone: ''
            });
            
            const editingSupplier = ref({});
            const deletingSupplier = ref(null);
            
            // Computed properties
            const filteredSuppliers = computed(() => {
                if (!searchQuery.value) return suppliers.value;
                
                const query = searchQuery.value.toLowerCase();
                return suppliers.value.filter(supplier => 
                    supplier.shortName.toLowerCase().includes(query) ||
                    supplier.fullName.toLowerCase().includes(query) ||
                    supplier.taxNumber.toLowerCase().includes(query) ||
                    supplier.city.toLowerCase().includes(query) ||
                    supplier.district.toLowerCase().includes(query)
                );
            });
            
            // Methods
            const fetchSuppliers = async () => {
                loading.value = true;
                try {
                    const response = await axios.get('/Supplier/GetAll');
                    suppliers.value = response.data;
                } catch (error) {
                    console.error('Error fetching suppliers:', error);
                    alert('Failed to load suppliers. Please try again.');
                } finally {
                    loading.value = false;
                }
            };
            
            const resetForm = () => {
                newSupplier.value = {
                    shortName: '',
                    fullName: '',
                    taxNumber: '',
                    district: '',
                    city: '',
                    fullAddress: '',
                    landlinePhone: '',
                    mobilePhone: ''
                };
                errors.value = {};
                similarSuppliers.value = [];
                activeSearchField.value = null;
            };
            
            const searchSimilarSuppliers = async (field) => {
                const value = newSupplier.value[field];
                activeSearchField.value = field;
                
                if (value.length < 2) {
                    similarSuppliers.value = [];
                    return;
                }
                
                try {
                    const response = await axios.get(`/Supplier/Search?query=${encodeURIComponent(value)}`);
                    similarSuppliers.value = response.data;
                } catch (error) {
                    console.error('Error searching suppliers:', error);
                    similarSuppliers.value = [];
                }
            };
            
            const selectSuggestion = (supplier, field) => {
                if (field === 'shortName') {
                    newSupplier.value.shortName = supplier.shortName;
                } else if (field === 'fullName') {
                    newSupplier.value.fullName = supplier.fullName;
                }
                similarSuppliers.value = [];
            };
            
            const createSupplier = async () => {
                errors.value = {};
                isSubmitting.value = true;
                
                try {
                    const response = await axios.post('/Supplier/Create', newSupplier.value);
                    suppliers.value.push(response.data);
                    showCreateModal.value = false;
                    resetForm();
                    alert('Supplier created successfully!');
                } catch (error) {
                    console.error('Error creating supplier:', error);
                    if (error.response && error.response.data) {
                        errors.value = error.response.data.errors || {};
                    } else {
                        alert('Failed to create supplier. Please try again.');
                    }
                } finally {
                    isSubmitting.value = false;
                }
            };
            
            const editSupplier = (supplier) => {
                editingSupplier.value = { ...supplier };
                showEditModal.value = true;
            };
            
            const updateSupplier = async () => {
                errors.value = {};
                isSubmitting.value = true;
                
                try {
                    const response = await axios.put(`/Supplier/Update?id=${editingSupplier.value.id}`, editingSupplier.value);
                    const index = suppliers.value.findIndex(s => s.id === editingSupplier.value.id);
                    if (index !== -1) {
                        suppliers.value[index] = response.data;
                    }
                    showEditModal.value = false;
                    alert('Supplier updated successfully!');
                } catch (error) {
                    console.error('Error updating supplier:', error);
                    if (error.response && error.response.data) {
                        errors.value = error.response.data.errors || {};
                    } else {
                        alert('Failed to update supplier. Please try again.');
                    }
                } finally {
                    isSubmitting.value = false;
                }
            };
            
            const deleteSupplier = (supplier) => {
                deletingSupplier.value = supplier;
                showDeleteModal.value = true;
            };
            
            const confirmDelete = async () => {
                if (!deletingSupplier.value) return;
                
                isSubmitting.value = true;
                
                try {
                    await axios.delete(`/Supplier/Delete?id=${deletingSupplier.value.id}`);
                    suppliers.value = suppliers.value.filter(s => s.id !== deletingSupplier.value.id);
                    showDeleteModal.value = false;
                    deletingSupplier.value = null;
                    alert('Supplier deleted successfully!');
                } catch (error) {
                    console.error('Error deleting supplier:', error);
                    alert('Failed to delete supplier. Please try again.');
                } finally {
                    isSubmitting.value = false;
                }
            };
            
            // Lifecycle hooks
            onMounted(fetchSuppliers);
            
            return {
                suppliers,
                loading,
                isSubmitting,
                searchQuery,
                errors,
                similarSuppliers,
                activeSearchField,
                showCreateModal,
                showEditModal,
                showDeleteModal,
                newSupplier,
                editingSupplier,
                deletingSupplier,
                filteredSuppliers,
                searchSimilarSuppliers,
                selectSuggestion,
                createSupplier,
                editSupplier,
                updateSupplier,
                deleteSupplier,
                confirmDelete
            };
        }
    }).mount('#supplier-app');
});
