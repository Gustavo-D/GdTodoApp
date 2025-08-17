import axios from 'axios';

const api = axios.create({
	baseURL: import.meta.env.URL_API,
});

function getAuthHeader() {
	const token = localStorage.getItem('token');
	return token ? { Authorization: `Bearer ${token}` } : {};
}

export const httpGet = async (endpoint: string, config = {}) => {
    const { headers, ...configLeft } = config as any
	const response = await api.get(endpoint, {
		...configLeft,
		headers: {
			...getAuthHeader(),
            ...headers,
		},
	});
	return response.data;
};

export const httpPost = async (endpoint: string, data: any, config = {}) => {
    const { headers, ...configLeft } = config as any
	const response = await api.post(endpoint, data, {
		...configLeft,
		headers: {
			...getAuthHeader(),
            ...headers,
		},
	});
	return response.data;
};

export const httpPatch = async (endpoint: string, data: any, config = {}) => {
    const { headers, ...configLeft } = config as any
	const response = await api.patch(endpoint, data, {
		...configLeft,
		headers: {
			...getAuthHeader(),
            ...headers,
		},
	});
	return response.data;
};

export const httpDelete = async (endpoint: string, config = {}) => {
    const { headers, ...configLeft } = config as any
	const response = await api.delete(endpoint, {
		...configLeft,
		headers: {
			...getAuthHeader(),
            ...headers,
		},
	});
	return response.data;
};
