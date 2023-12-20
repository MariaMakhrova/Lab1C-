import { universities } from "@/db"; // Шлях до файлу 

const state = {
  universities: [...universities], // Початковий стан 
};

const mutations = {
  SET_UNIVERSITIES(state, universities) {
    state.universities = universities;
  },
  ADD_UNIVERSITY(state, university) {
    state.universities.push(university);
  },
  DELETE_UNIVERSITY(state, universityId) {
    state.universities = state.universities.filter(u => u.id !== universityId);
  },
};

const actions = {
  createUniversity({ commit }, university) {
    commit("ADD_UNIVERSITY", university);
  },
  removeUniversity({ commit }, universityId) {
    commit("DELETE_UNIVERSITY", universityId);
  },
};

const getters = {
  allUniversities: state => state.universities,
  filteredUniversities: state => {
    // Приклад фільтрації
    return state.universities.filter(u => u.rating > 4.0);
  },
  getUniversityById: state => id => {
    return state.universities.find(u => u.id === id);
  },
};

export default {
  state,
  mutations,
  actions,
  getters,
};