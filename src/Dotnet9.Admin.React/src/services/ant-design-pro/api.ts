// @ts-ignore
/* eslint-disable */
import { request } from '@umijs/max';

/** 获取当前的用户 GET /api/currentUser */
export async function currentUser(options?: { [key: string]: any }) {
  return request<{
    data: API.CurrentUser;
  }>('/api/login/currentUser', {
    method: 'GET',
    ...(options || {}),
  });
}

/** 退出登录接口 POST /api/login/outLogin */
export async function outLogin(options?: { [key: string]: any }) {
  return request<Record<string, any>>('/api/login/outLogin', {
    method: 'POST',
    ...(options || {}),
  });
}

/** 登录接口 POST /api/login/account */
export async function login(body: API.LoginParams, options?: { [key: string]: any }) {
  return request<API.LoginResult>('/api/login/account', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    data: body,
    ...(options || {}),
  });
}

/** 此处后端没有提供注释 GET /api/notices */
export async function getNotices(options?: { [key: string]: any }) {
  return request<API.NoticeIconList>('/api/notices', {
    method: 'GET',
    ...(options || {}),
  });
}

/** 获取链接列表 GET /api/kubj */
export async function link(
  params: {
    // query
    /** 当前的页码 */
    current?: number;
    /** 页面的容量 */
    pageSize?: number;
  },
  options?: { [key: string]: any },
) {
  return request<API.LinkList>('/api/link', {
    method: 'GET',
    params: {
      ...params,
    },
    ...(options || {}),
  });
}

/** 更新链接 PUT /api/link */
export async function updateLink(body: API.LinkListItem, options?: { [key: string]: any }) {
  return request<API.LinkListItem>(`/api/link/${body.id}`, {
    method: 'PUT',
    data: body,
    ...(options || {}),
  });
}

/** 新建链接 POST /api/link */
export async function addLink(body: API.LinkListItem, options?: { [key: string]: any }) {
  return request<API.LinkListItem>('/api/link', {
    method: 'POST',
    data: body,
    ...(options || {}),
  });
}

/** 删除链接 DELETE /api/link */
export async function removeLink(data: string[], options?: { [key: string]: any }) {
  return request<Record<string, any>>('/api/link', {
    method: 'DELETE',
    data: { ids: data },
    ...(options || {}),
  });
}

/** 获取操作日志列表 GET /api/actionlog */
export async function actionLog(
  params: {
    // query
    /** 当前的页码 */
    current?: number;
    /** 页面的容量 */
    pageSize?: number;
  },
  sort: { [key: string]: any },
  options?: { [key: string]: any },
) {
  return request<API.ActionLogList>('/api/actionlog', {
    method: 'GET',
    params: {
      ...params,
      sort,
    },
    ...(options || {}),
  });
}

/** 删除操作日志 DELETE /api/actionlog */
export async function removeActionLog(data: string[], options?: { [key: string]: any }) {
  return request<Record<string, any>>('/api/actionlog', {
    method: 'DELETE',
    data: { ids: data },
    ...(options || {}),
  });
}