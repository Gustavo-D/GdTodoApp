import axios from 'axios';

const api = axios.create({
	baseURL: 'http://localhost:5189/api/'
});

function getAuthHeader() {
	const token = localStorage.getItem('token');
	return token ? { Authorization: `Bearer ${token}` } : {};
}

export const httpGet = async (endpoint: string, params?: {}) => {
	try {
		const response = await api.get(endpoint, {
			params: params,
			paramsSerializer: {
				indexes: null
			},
			headers: getAuthHeader(),
		});
		return response.data;
	} catch (error: any) {
		return null;
	}
};

export const httpPost = async (
    endpoint: string, 
    data: any, 
    onSuccess?: (data: any) => Promise<any>,
    onError?: (data: any) => Promise<any> ): Promise<void> => {
    try {
        const response = await api.post(endpoint, data, {
            headers: getAuthHeader()
        });
        if(onSuccess)
            await onSuccess(response.data);
    } catch (error: any) {
        if(onError)
            onError(error.response.data.Meta.Errors[0].FriendlyErrorMessage);
    }    
};

export const httpPatch = async (
	endpoint: string, 
	data: any, 
    onSuccess?: (data: any) => Promise<any>,
    onError?: (data: any) => Promise<any>) => {
    try {
        const response = await api.patch(endpoint, data, {
            headers: getAuthHeader()
        });
        if(onSuccess)
            await onSuccess(response.data);
    } catch (error: any) {
        if(onError)
            onError(error.response.data.Meta.Errors[0].FriendlyErrorMessage);
    }    
};

export const httpDelete = async (
	endpoint: string, 
    onSuccess?: (data: any) => Promise<any>,
    onError?: (data: any) => Promise<any>) => {
    try {
        const response = await api.delete(endpoint, {
            headers: getAuthHeader()
        });
        if(onSuccess)
            await onSuccess(response.data);
    } catch (error: any) {
        if(onError)
            onError(error.response.data.Meta.Errors[0].FriendlyErrorMessage);
    }    
};
