import axios from 'axios';

const BASE_URL = ''; // API URL'inizi buraya ekleyin, yoksa boş bırakın

// Axios instance oluştur
const api = axios.create({
  baseURL: BASE_URL,
  headers: {
    'Content-Type': 'application/json'
  }
});

// Test verileri
const testData = {
  suppliers: [
    {
      id: 1,
      shortName: 'ABC Ltd',
      fullName: 'ABC Limited Şirketi',
      taxNumber: '1234567890',
      address: 'İstanbul, Türkiye',
      phone: '0212 123 4567',
      email: 'info@abc.com',
      notes: 'Test tedarikçi',
      isActive: true
    },
    {
      id: 2,
      shortName: 'XYZ A.Ş.',
      fullName: 'XYZ Anonim Şirketi',
      taxNumber: '0987654321',
      address: 'Ankara, Türkiye',
      phone: '0312 987 6543',
      email: 'info@xyz.com',
      notes: 'Ana tedarikçi',
      isActive: true
    }
  ],
  mainCategories: [
    { id: 1, code: '01', name: 'Elektronik', description: 'Elektronik ürünler' },
    { id: 2, code: '02', name: 'Mekanik', description: 'Mekanik parçalar' },
    { id: 3, code: '03', name: 'Ofis', description: 'Ofis malzemeleri' }
  ],
  subCategories: [
    { id: 1, mainCategoryId: 1, code: '01.01', name: 'Bilgisayarlar', description: 'PC, Laptop' },
    { id: 2, mainCategoryId: 1, code: '01.02', name: 'Telefonlar', description: 'Mobil cihazlar' },
    { id: 3, mainCategoryId: 2, code: '02.01', name: 'Motorlar', description: 'DC ve AC motorlar' },
    { id: 4, mainCategoryId: 3, code: '03.01', name: 'Kağıt Ürünleri', description: 'Kağıt, defter, vs.' }
  ],
  materialTypes: [
    { id: 1, subCategoryId: 1, code: '01.01.01', name: 'Laptoplar' },
    { id: 2, subCategoryId: 1, code: '01.01.02', name: 'Masaüstü Bilgisayarlar' },
    { id: 3, subCategoryId: 2, code: '01.02.01', name: 'Akıllı Telefonlar' },
    { id: 4, subCategoryId: 3, code: '02.01.01', name: 'DC Motorlar' },
    { id: 5, subCategoryId: 4, code: '03.01.01', name: 'A4 Kağıt' }
  ],
  materials: [
    {
      id: 1,
      code: '01.01.01.001',
      name: 'Dell XPS 13',
      materialTypeId: 1,
      unit: 'Adet',
      stockQuantity: 10,
      price: 35000,
      minStockLevel: 2,
      supplierId: 1,
      description: '13 inç laptop',
      isActive: true
    },
    {
      id: 2,
      code: '01.02.01.001',
      name: 'iPhone 13',
      materialTypeId: 3,
      unit: 'Adet',
      stockQuantity: 5,
      price: 25000,
      minStockLevel: 1,
      supplierId: 2,
      description: 'Apple telefon',
      isActive: true
    },
    {
      id: 3,
      code: '02.01.01.001',
      name: '12V DC Motor',
      materialTypeId: 4,
      unit: 'Adet',
      stockQuantity: 50,
      price: 150,
      minStockLevel: 10,
      supplierId: 1,
      description: '12V 3A motor',
      isActive: true
    },
    {
      id: 4,
      code: '03.01.01.001',
      name: 'A4 Kağıt 80gr',
      materialTypeId: 5,
      unit: 'Paket',
      stockQuantity: 100,
      price: 120,
      minStockLevel: 20,
      supplierId: 2,
      description: '500 sayfa, 80gr',
      isActive: true
    }
  ],
  departments: [
    { id: 1, name: 'Satış' },
    { id: 2, name: 'Üretim' },
    { id: 3, name: 'Ar-Ge' },
    { id: 4, name: 'Satın Alma' },
    { id: 5, name: 'Muhasebe' }
  ],
  customerTeams: [
    {
      id: 1,
      code: 'MUS001',
      name: 'ABC Müşteri',
      departmentId: 1,
      contactPerson: 'Ahmet Yılmaz',
      phone: '0212 555 4433',
      email: 'ahmet@abc.com',
      address: 'İstanbul',
      notes: 'Önemli müşteri',
      isActive: true
    },
    {
      id: 2,
      code: 'EKP001',
      name: 'Ar-Ge Ekibi',
      departmentId: 3,
      contactPerson: 'Mehmet Demir',
      phone: '0212 555 6677',
      email: 'mehmet@firma.com',
      address: 'İstanbul',
      notes: 'Ana Ar-Ge ekibi',
      isActive: true
    },
    {
      id: 3,
      code: 'MUS002',
      name: 'XYZ Müşteri',
      departmentId: 1,
      contactPerson: 'Ayşe Kaya',
      phone: '0212 555 8899',
      email: 'ayse@xyz.com',
      address: 'Ankara',
      notes: 'Düzenli sipariş',
      isActive: true
    }
  ]
};

// API yok ise, test verileriyle mock bir yanıt döndür
// API var ise, gerçek API çağrısını yap
const apiService = {
  // Supplier (Tedarikçi) işlemleri
  async getSuppliers() {
    try {
      if (BASE_URL) {
        const response = await api.get('/suppliers');
        return response.data;
      } else {
        return testData.suppliers;
      }
    } catch (error) {
      console.error('Tedarikçiler alınırken hata oluştu:', error);
      return testData.suppliers;
    }
  },
  
  async createSupplier(supplier) {
    try {
      if (BASE_URL) {
        const response = await api.post('/suppliers', supplier);
        return response.data;
      } else {
        const newId = Math.max(0, ...testData.suppliers.map(s => s.id)) + 1;
        const newSupplier = { ...supplier, id: newId };
        testData.suppliers.push(newSupplier);
        return newSupplier;
      }
    } catch (error) {
      console.error('Tedarikçi oluşturulurken hata oluştu:', error);
      throw error;
    }
  },
  
  async updateSupplier(id, supplier) {
    try {
      if (BASE_URL) {
        const response = await api.put(`/suppliers/${id}`, supplier);
        return response.data;
      } else {
        const index = testData.suppliers.findIndex(s => s.id === id);
        if (index === -1) throw new Error('Tedarikçi bulunamadı');
        
        testData.suppliers[index] = { ...testData.suppliers[index], ...supplier };
        return testData.suppliers[index];
      }
    } catch (error) {
      console.error('Tedarikçi güncellenirken hata oluştu:', error);
      throw error;
    }
  },
  
  async deleteSupplier(id) {
    try {
      if (BASE_URL) {
        await api.delete(`/suppliers/${id}`);
        return true;
      } else {
        const index = testData.suppliers.findIndex(s => s.id === id);
        if (index === -1) throw new Error('Tedarikçi bulunamadı');
        
        testData.suppliers.splice(index, 1);
        return true;
      }
    } catch (error) {
      console.error('Tedarikçi silinirken hata oluştu:', error);
      throw error;
    }
  },
  
  // MainCategory (Ana Kategori) işlemleri
  async getMainCategories() {
    try {
      if (BASE_URL) {
        const response = await api.get('/main-categories');
        return response.data;
      } else {
        return testData.mainCategories;
      }
    } catch (error) {
      console.error('Ana kategoriler alınırken hata oluştu:', error);
      return testData.mainCategories;
    }
  },
  
  async createMainCategory(category) {
    try {
      if (BASE_URL) {
        const response = await api.post('/main-categories', category);
        return response.data;
      } else {
        const newId = Math.max(0, ...testData.mainCategories.map(c => c.id)) + 1;
        const newCategory = { ...category, id: newId };
        testData.mainCategories.push(newCategory);
        return newCategory;
      }
    } catch (error) {
      console.error('Ana kategori oluşturulurken hata oluştu:', error);
      throw error;
    }
  },
  
  async updateMainCategory(id, category) {
    try {
      if (BASE_URL) {
        const response = await api.put(`/main-categories/${id}`, category);
        return response.data;
      } else {
        const index = testData.mainCategories.findIndex(c => c.id === id);
        if (index === -1) throw new Error('Ana kategori bulunamadı');
        
        testData.mainCategories[index] = { ...testData.mainCategories[index], ...category };
        return testData.mainCategories[index];
      }
    } catch (error) {
      console.error('Ana kategori güncellenirken hata oluştu:', error);
      throw error;
    }
  },
  
  async deleteMainCategory(id) {
    try {
      if (BASE_URL) {
        await api.delete(`/main-categories/${id}`);
        return true;
      } else {
        const index = testData.mainCategories.findIndex(c => c.id === id);
        if (index === -1) throw new Error('Ana kategori bulunamadı');
        
        // İlişkili alt kategorileri de sil
        testData.subCategories = testData.subCategories.filter(sc => sc.mainCategoryId !== id);
        
        testData.mainCategories.splice(index, 1);
        return true;
      }
    } catch (error) {
      console.error('Ana kategori silinirken hata oluştu:', error);
      throw error;
    }
  },
  
  // SubCategory (Alt Kategori) işlemleri
  async getSubCategories() {
    try {
      if (BASE_URL) {
        const response = await api.get('/sub-categories');
        return response.data;
      } else {
        return testData.subCategories;
      }
    } catch (error) {
      console.error('Alt kategoriler alınırken hata oluştu:', error);
      return testData.subCategories;
    }
  },
  
  async createSubCategory(category) {
    try {
      if (BASE_URL) {
        const response = await api.post('/sub-categories', category);
        return response.data;
      } else {
        const newId = Math.max(0, ...testData.subCategories.map(c => c.id)) + 1;
        const newCategory = { ...category, id: newId };
        testData.subCategories.push(newCategory);
        return newCategory;
      }
    } catch (error) {
      console.error('Alt kategori oluşturulurken hata oluştu:', error);
      throw error;
    }
  },
  
  async updateSubCategory(id, category) {
    try {
      if (BASE_URL) {
        const response = await api.put(`/sub-categories/${id}`, category);
        return response.data;
      } else {
        const index = testData.subCategories.findIndex(c => c.id === id);
        if (index === -1) throw new Error('Alt kategori bulunamadı');
        
        testData.subCategories[index] = { ...testData.subCategories[index], ...category };
        return testData.subCategories[index];
      }
    } catch (error) {
      console.error('Alt kategori güncellenirken hata oluştu:', error);
      throw error;
    }
  },
  
  async deleteSubCategory(id) {
    try {
      if (BASE_URL) {
        await api.delete(`/sub-categories/${id}`);
        return true;
      } else {
        const index = testData.subCategories.findIndex(c => c.id === id);
        if (index === -1) throw new Error('Alt kategori bulunamadı');
        
        testData.subCategories.splice(index, 1);
        return true;
      }
    } catch (error) {
      console.error('Alt kategori silinirken hata oluştu:', error);
      throw error;
    }
  },
  
  // Material işlemleri
  async getMaterials() {
    try {
      if (BASE_URL) {
        const response = await api.get('/materials');
        return response.data;
      } else {
        return testData.materials;
      }
    } catch (error) {
      console.error('Malzemeler alınırken hata oluştu:', error);
      return testData.materials;
    }
  },
  
  async createMaterial(material) {
    try {
      if (BASE_URL) {
        const response = await api.post('/materials', material);
        return response.data;
      } else {
        const newId = Math.max(0, ...testData.materials.map(m => m.id)) + 1;
        const newMaterial = { ...material, id: newId };
        testData.materials.push(newMaterial);
        return newMaterial;
      }
    } catch (error) {
      console.error('Malzeme oluşturulurken hata oluştu:', error);
      throw error;
    }
  },
  
  async updateMaterial(id, material) {
    try {
      if (BASE_URL) {
        const response = await api.put(`/materials/${id}`, material);
        return response.data;
      } else {
        const index = testData.materials.findIndex(m => m.id === id);
        if (index === -1) throw new Error('Malzeme bulunamadı');
        
        testData.materials[index] = { ...testData.materials[index], ...material };
        return testData.materials[index];
      }
    } catch (error) {
      console.error('Malzeme güncellenirken hata oluştu:', error);
      throw error;
    }
  },
  
  async deleteMaterial(id) {
    try {
      if (BASE_URL) {
        await api.delete(`/materials/${id}`);
        return true;
      } else {
        const index = testData.materials.findIndex(m => m.id === id);
        if (index === -1) throw new Error('Malzeme bulunamadı');
        
        testData.materials.splice(index, 1);
        return true;
      }
    } catch (error) {
      console.error('Malzeme silinirken hata oluştu:', error);
      throw error;
    }
  },
  
  // MaterialType işlemleri
  async getMaterialTypes() {
    try {
      if (BASE_URL) {
        const response = await api.get('/material-types');
        return response.data;
      } else {
        return testData.materialTypes;
      }
    } catch (error) {
      console.error('Malzeme tipleri alınırken hata oluştu:', error);
      return testData.materialTypes;
    }
  },
  
  // CustomerTeam işlemleri
  async getCustomerTeams() {
    try {
      if (BASE_URL) {
        const response = await api.get('/customer-teams');
        return response.data;
      } else {
        return testData.customerTeams;
      }
    } catch (error) {
      console.error('Müşteri/Ekipler alınırken hata oluştu:', error);
      return testData.customerTeams;
    }
  },
  
  async createCustomerTeam(team) {
    try {
      if (BASE_URL) {
        const response = await api.post('/customer-teams', team);
        return response.data;
      } else {
        const newId = Math.max(0, ...testData.customerTeams.map(t => t.id)) + 1;
        const newTeam = { ...team, id: newId };
        testData.customerTeams.push(newTeam);
        return newTeam;
      }
    } catch (error) {
      console.error('Müşteri/Ekip oluşturulurken hata oluştu:', error);
      throw error;
    }
  },
  
  async updateCustomerTeam(id, team) {
    try {
      if (BASE_URL) {
        const response = await api.put(`/customer-teams/${id}`, team);
        return response.data;
      } else {
        const index = testData.customerTeams.findIndex(t => t.id === id);
        if (index === -1) throw new Error('Müşteri/Ekip bulunamadı');
        
        testData.customerTeams[index] = { ...testData.customerTeams[index], ...team };
        return testData.customerTeams[index];
      }
    } catch (error) {
      console.error('Müşteri/Ekip güncellenirken hata oluştu:', error);
      throw error;
    }
  },
  
  async deleteCustomerTeam(id) {
    try {
      if (BASE_URL) {
        await api.delete(`/customer-teams/${id}`);
        return true;
      } else {
        const index = testData.customerTeams.findIndex(t => t.id === id);
        if (index === -1) throw new Error('Müşteri/Ekip bulunamadı');
        
        testData.customerTeams.splice(index, 1);
        return true;
      }
    } catch (error) {
      console.error('Müşteri/Ekip silinirken hata oluştu:', error);
      throw error;
    }
  },
  
  // Department işlemleri
  async getDepartments() {
    try {
      if (BASE_URL) {
        const response = await api.get('/departments');
        return response.data;
      } else {
        return testData.departments;
      }
    } catch (error) {
      console.error('Departmanlar alınırken hata oluştu:', error);
      return testData.departments;
    }
  }
};

export default apiService;
