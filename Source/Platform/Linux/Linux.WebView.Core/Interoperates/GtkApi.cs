using System.Runtime.CompilerServices;
using static Linux.WebView.Core.LinuxApplicationManager;

namespace Linux.WebView.Core.Interoperates;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void gdk_set_allowed_backends_delegate(string backends);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate nint gdk_x11_window_get_xid_delegate(nint gdkWindowHandle);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void gtk_widget_realize_delegate(nint widgetHandle);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate ulong g_signal_connect_data_delegate(nint instance, string detailed_signal, nint c_handler, nint data, nint destroy_data, GConnectFlags connect_flags);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate nint g_memory_input_stream_new_from_data_delegate(byte[] data, uint length, nint destroy);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate nint webkit_user_script_new_delegate(string script, WebKitUserContentInjectedFrames injected_frames, WebKitUserScriptInjectionTime injection_time, string? allow_list, string? block_list);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void webkit_user_script_unref_delegate(nint scriptHandle);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void webkit_user_content_manager_add_script_delegate(nint userContentManagerInstance, nint script);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate bool webkit_user_content_manager_register_script_message_handler_delegate(nint userContentManagerInstance, string name);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate nint webkit_javascript_result_get_js_value_delegate(nint jsResult);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void webkit_javascript_result_unref_delegate(nint jsResult);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate bool jsc_value_is_string_delegate(nint value);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate nint jsc_value_to_string_delegate(nint value);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void webkit_policy_decision_ignore_delegate(nint decision);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void webkit_policy_decision_use_delegate(nint decision);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate nint webkit_web_view_new_delegate();

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate nint webkit_web_view_get_settings_delegate(nint webView);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate nint webkit_web_view_get_user_content_manager_delegate(nint webView);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate nint webkit_web_view_get_context_delegate(nint webView);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void webkit_web_view_load_uri_delegate(nint webView, string uri);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void webkit_web_view_load_html_delegate(nint webView, string content, string? baseUri);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void webkit_web_view_go_back_delegate(nint webView);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void webkit_web_view_go_forward_delegate(nint webView);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate bool webkit_web_view_can_go_back_delegate(nint webView);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate bool webkit_web_view_can_go_forward_delegate(nint webView);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void webkit_web_view_reload_delegate(nint webView);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void webkit_web_view_stop_loading_delegate(nint webView);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void webkit_web_view_run_javascript_delegate(nint webView, string script, nint cancellable, nint callback, nint userData);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate double webkit_web_view_get_zoom_level_delegate(nint webView);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void webkit_web_view_set_zoom_level_delegate(nint webView, double zoomLevel);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void webkit_settings_set_enable_developer_extras_delegate(nint settings, bool enabled);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void webkit_settings_set_allow_file_access_from_file_urls_delegate(nint settings, bool allowed);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void webkit_settings_set_allow_modal_dialogs_delegate(nint settings, bool allowed);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void webkit_settings_set_allow_top_navigation_to_data_urls_delegate(nint settings, bool allowed);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void webkit_settings_set_allow_universal_access_from_file_urls_delegate(nint settings, bool allowed);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void webkit_settings_set_enable_back_forward_navigation_gestures_delegate(nint settings, bool enabled);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void webkit_settings_set_enable_caret_browsing_delegate(nint settings, bool enabled);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void webkit_settings_set_enable_media_capabilities_delegate(nint settings, bool enabled);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void webkit_settings_set_enable_media_stream_delegate(nint settings, bool enabled);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void webkit_settings_set_javascript_can_access_clipboard_delegate(nint settings, bool allowed);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void webkit_settings_set_javascript_can_open_windows_automatically_delegate(nint settings, bool allowed);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void webkit_settings_set_enable_fullscreen_delegate(nint settings, bool enabled);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void webkit_web_context_register_uri_scheme_delegate(nint context, string scheme, nint callback, nint userData, nint destroyNotify);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate nint webkit_uri_scheme_request_get_scheme_delegate(nint request);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate nint webkit_uri_scheme_request_get_uri_delegate(nint request);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate nint webkit_uri_scheme_request_get_path_delegate(nint request);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void webkit_uri_scheme_request_finish_delegate(nint request, nint stream, long streamLength, string? contentType);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate nint webkit_navigation_policy_decision_get_navigation_action_delegate(nint decision);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate nint webkit_navigation_policy_decision_get_request_delegate(nint decision);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate nint webkit_navigation_action_get_request_delegate(nint navigationAction);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate nint webkit_uri_request_get_uri_delegate(nint request);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void webkit_permission_request_allow_delegate(nint request);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void uri_scheme_request_callback_delegate(nint request, nint userData);

public static class GtkApi
{
    static GtkApi()
    {
        __gdk_set_allowed_backends = LoadDelegate<gdk_set_allowed_backends_delegate>(gLibrary.Gdk, gdk_set_allowed_backends)!;
        __gdk_x11_window_get_xid = LoadDelegate<gdk_x11_window_get_xid_delegate>(gLibrary.Gdk, gdk_x11_window_get_xid)!;

        __gtk_widget_realize_delegate = LoadDelegate<gtk_widget_realize_delegate>(gLibrary.Gtk, gtk_widget_realize)!;
        __g_signal_connect_data = LoadDelegate<g_signal_connect_data_delegate>(gLibrary.Gtk, g_signal_connect_data)!;
        __g_memory_input_stream_new_from_data = LoadDelegate<g_memory_input_stream_new_from_data_delegate>(gLibrary.Gtk, g_memory_input_stream_new_from_data)!;

        __webkit_user_script_new = LoadDelegate<webkit_user_script_new_delegate>(gLibrary.Webkit, webkit_user_script_new)!;
        __webkit_user_script_unref = LoadDelegate<webkit_user_script_unref_delegate>(gLibrary.Webkit, webkit_user_script_unref)!;
        __webkit_user_content_manager_add_script = LoadDelegate<webkit_user_content_manager_add_script_delegate>(gLibrary.Webkit, webkit_user_content_manager_add_script)!;
        __webkit_user_content_manager_register_script_message_handler = LoadDelegate<webkit_user_content_manager_register_script_message_handler_delegate>(gLibrary.Webkit, webkit_user_content_manager_register_script_message_handler)!;

        // JSC functions - try webkit library first, then JavaScriptCore library
        __jsc_value_is_string = LoadDelegate<jsc_value_is_string_delegate>(gLibrary.Webkit, jsc_value_is_string)
                                ?? LoadDelegate<jsc_value_is_string_delegate>(gLibrary.JavaScriptCore, jsc_value_is_string);
        __jsc_value_to_string = LoadDelegate<jsc_value_to_string_delegate>(gLibrary.Webkit, jsc_value_to_string)
                                ?? LoadDelegate<jsc_value_to_string_delegate>(gLibrary.JavaScriptCore, jsc_value_to_string);

        // webkit_javascript_result_* only exists in webkit2gtk-4.0, not in 4.1
        __webkit_javascript_result_get_js_value = LoadDelegate<webkit_javascript_result_get_js_value_delegate>(gLibrary.Webkit, webkit_javascript_result_get_js_value);
        __webkit_javascript_result_unref = LoadDelegate<webkit_javascript_result_unref_delegate>(gLibrary.Webkit, webkit_javascript_result_unref);

        __webkit_policy_decision_ignore = LoadDelegate<webkit_policy_decision_ignore_delegate>(gLibrary.Webkit, webkit_policy_decision_ignore)!;
        __webkit_policy_decision_use = LoadDelegate<webkit_policy_decision_use_delegate>(gLibrary.Webkit, webkit_policy_decision_use)!;

        // WebView functions
        __webkit_web_view_new = LoadDelegate<webkit_web_view_new_delegate>(gLibrary.Webkit, webkit_web_view_new)!;
        __webkit_web_view_get_settings = LoadDelegate<webkit_web_view_get_settings_delegate>(gLibrary.Webkit, webkit_web_view_get_settings)!;
        __webkit_web_view_get_user_content_manager = LoadDelegate<webkit_web_view_get_user_content_manager_delegate>(gLibrary.Webkit, webkit_web_view_get_user_content_manager)!;
        __webkit_web_view_get_context = LoadDelegate<webkit_web_view_get_context_delegate>(gLibrary.Webkit, webkit_web_view_get_context)!;
        __webkit_web_view_load_uri = LoadDelegate<webkit_web_view_load_uri_delegate>(gLibrary.Webkit, webkit_web_view_load_uri)!;
        __webkit_web_view_load_html = LoadDelegate<webkit_web_view_load_html_delegate>(gLibrary.Webkit, webkit_web_view_load_html)!;
        __webkit_web_view_go_back = LoadDelegate<webkit_web_view_go_back_delegate>(gLibrary.Webkit, webkit_web_view_go_back)!;
        __webkit_web_view_go_forward = LoadDelegate<webkit_web_view_go_forward_delegate>(gLibrary.Webkit, webkit_web_view_go_forward)!;
        __webkit_web_view_can_go_back = LoadDelegate<webkit_web_view_can_go_back_delegate>(gLibrary.Webkit, webkit_web_view_can_go_back)!;
        __webkit_web_view_can_go_forward = LoadDelegate<webkit_web_view_can_go_forward_delegate>(gLibrary.Webkit, webkit_web_view_can_go_forward)!;
        __webkit_web_view_reload = LoadDelegate<webkit_web_view_reload_delegate>(gLibrary.Webkit, webkit_web_view_reload)!;
        __webkit_web_view_stop_loading = LoadDelegate<webkit_web_view_stop_loading_delegate>(gLibrary.Webkit, webkit_web_view_stop_loading)!;
        __webkit_web_view_run_javascript = LoadDelegate<webkit_web_view_run_javascript_delegate>(gLibrary.Webkit, webkit_web_view_run_javascript)!;
        __webkit_web_view_get_zoom_level = LoadDelegate<webkit_web_view_get_zoom_level_delegate>(gLibrary.Webkit, webkit_web_view_get_zoom_level)!;
        __webkit_web_view_set_zoom_level = LoadDelegate<webkit_web_view_set_zoom_level_delegate>(gLibrary.Webkit, webkit_web_view_set_zoom_level)!;

        // Settings
        __webkit_settings_set_enable_developer_extras = LoadDelegate<webkit_settings_set_enable_developer_extras_delegate>(gLibrary.Webkit, webkit_settings_set_enable_developer_extras)!;
        __webkit_settings_set_allow_file_access_from_file_urls = LoadDelegate<webkit_settings_set_allow_file_access_from_file_urls_delegate>(gLibrary.Webkit, webkit_settings_set_allow_file_access_from_file_urls)!;
        __webkit_settings_set_allow_modal_dialogs = LoadDelegate<webkit_settings_set_allow_modal_dialogs_delegate>(gLibrary.Webkit, webkit_settings_set_allow_modal_dialogs)!;
        __webkit_settings_set_allow_top_navigation_to_data_urls = LoadDelegate<webkit_settings_set_allow_top_navigation_to_data_urls_delegate>(gLibrary.Webkit, webkit_settings_set_allow_top_navigation_to_data_urls);
        __webkit_settings_set_allow_universal_access_from_file_urls = LoadDelegate<webkit_settings_set_allow_universal_access_from_file_urls_delegate>(gLibrary.Webkit, webkit_settings_set_allow_universal_access_from_file_urls)!;
        __webkit_settings_set_enable_back_forward_navigation_gestures = LoadDelegate<webkit_settings_set_enable_back_forward_navigation_gestures_delegate>(gLibrary.Webkit, webkit_settings_set_enable_back_forward_navigation_gestures)!;
        __webkit_settings_set_enable_caret_browsing = LoadDelegate<webkit_settings_set_enable_caret_browsing_delegate>(gLibrary.Webkit, webkit_settings_set_enable_caret_browsing)!;
        __webkit_settings_set_enable_media_capabilities = LoadDelegate<webkit_settings_set_enable_media_capabilities_delegate>(gLibrary.Webkit, webkit_settings_set_enable_media_capabilities);
        __webkit_settings_set_enable_media_stream = LoadDelegate<webkit_settings_set_enable_media_stream_delegate>(gLibrary.Webkit, webkit_settings_set_enable_media_stream)!;
        __webkit_settings_set_javascript_can_access_clipboard = LoadDelegate<webkit_settings_set_javascript_can_access_clipboard_delegate>(gLibrary.Webkit, webkit_settings_set_javascript_can_access_clipboard)!;
        __webkit_settings_set_javascript_can_open_windows_automatically = LoadDelegate<webkit_settings_set_javascript_can_open_windows_automatically_delegate>(gLibrary.Webkit, webkit_settings_set_javascript_can_open_windows_automatically)!;
        __webkit_settings_set_enable_fullscreen = LoadDelegate<webkit_settings_set_enable_fullscreen_delegate>(gLibrary.Webkit, webkit_settings_set_enable_fullscreen)!;

        // URI scheme
        __webkit_web_context_register_uri_scheme = LoadDelegate<webkit_web_context_register_uri_scheme_delegate>(gLibrary.Webkit, webkit_web_context_register_uri_scheme)!;
        __webkit_uri_scheme_request_get_scheme = LoadDelegate<webkit_uri_scheme_request_get_scheme_delegate>(gLibrary.Webkit, webkit_uri_scheme_request_get_scheme)!;
        __webkit_uri_scheme_request_get_uri = LoadDelegate<webkit_uri_scheme_request_get_uri_delegate>(gLibrary.Webkit, webkit_uri_scheme_request_get_uri)!;
        __webkit_uri_scheme_request_get_path = LoadDelegate<webkit_uri_scheme_request_get_path_delegate>(gLibrary.Webkit, webkit_uri_scheme_request_get_path);
        __webkit_uri_scheme_request_finish = LoadDelegate<webkit_uri_scheme_request_finish_delegate>(gLibrary.Webkit, webkit_uri_scheme_request_finish)!;

        // Navigation
        __webkit_navigation_policy_decision_get_navigation_action = LoadDelegate<webkit_navigation_policy_decision_get_navigation_action_delegate>(gLibrary.Webkit, webkit_navigation_policy_decision_get_navigation_action);
        __webkit_navigation_policy_decision_get_request = LoadDelegate<webkit_navigation_policy_decision_get_request_delegate>(gLibrary.Webkit, webkit_navigation_policy_decision_get_request);
        __webkit_navigation_action_get_request = LoadDelegate<webkit_navigation_action_get_request_delegate>(gLibrary.Webkit, webkit_navigation_action_get_request);
        __webkit_uri_request_get_uri = LoadDelegate<webkit_uri_request_get_uri_delegate>(gLibrary.Webkit, webkit_uri_request_get_uri)!;
        __webkit_permission_request_allow = LoadDelegate<webkit_permission_request_allow_delegate>(gLibrary.Webkit, webkit_permission_request_allow)!;
    }

    private static string gdk_set_allowed_backends => nameof(gdk_set_allowed_backends);
    private static string gtk_widget_realize => nameof(gtk_widget_realize);
    private static string gdk_x11_window_get_xid => nameof(gdk_x11_window_get_xid);
    private static string g_signal_connect_data => nameof(g_signal_connect_data);
    private static string g_memory_input_stream_new_from_data => nameof(g_memory_input_stream_new_from_data);
    private static string webkit_user_script_new => nameof(webkit_user_script_new);
    private static string webkit_user_script_unref => nameof(webkit_user_script_unref);
    private static string webkit_user_content_manager_add_script => nameof(webkit_user_content_manager_add_script);
    private static string webkit_user_content_manager_register_script_message_handler => nameof(webkit_user_content_manager_register_script_message_handler);
    private static string webkit_javascript_result_get_js_value => nameof(webkit_javascript_result_get_js_value);
    private static string webkit_javascript_result_unref => nameof(webkit_javascript_result_unref);
    private static string jsc_value_is_string => nameof(jsc_value_is_string);
    private static string jsc_value_to_string => nameof(jsc_value_to_string);
    private static string webkit_policy_decision_ignore => nameof(webkit_policy_decision_ignore);
    private static string webkit_policy_decision_use => nameof(webkit_policy_decision_use);
    private static string webkit_web_view_new => nameof(webkit_web_view_new);
    private static string webkit_web_view_get_settings => nameof(webkit_web_view_get_settings);
    private static string webkit_web_view_get_user_content_manager => nameof(webkit_web_view_get_user_content_manager);
    private static string webkit_web_view_get_context => nameof(webkit_web_view_get_context);
    private static string webkit_web_view_load_uri => nameof(webkit_web_view_load_uri);
    private static string webkit_web_view_load_html => nameof(webkit_web_view_load_html);
    private static string webkit_web_view_go_back => nameof(webkit_web_view_go_back);
    private static string webkit_web_view_go_forward => nameof(webkit_web_view_go_forward);
    private static string webkit_web_view_can_go_back => nameof(webkit_web_view_can_go_back);
    private static string webkit_web_view_can_go_forward => nameof(webkit_web_view_can_go_forward);
    private static string webkit_web_view_reload => nameof(webkit_web_view_reload);
    private static string webkit_web_view_stop_loading => nameof(webkit_web_view_stop_loading);
    private static string webkit_web_view_run_javascript => nameof(webkit_web_view_run_javascript);
    private static string webkit_web_view_get_zoom_level => nameof(webkit_web_view_get_zoom_level);
    private static string webkit_web_view_set_zoom_level => nameof(webkit_web_view_set_zoom_level);
    private static string webkit_settings_set_enable_developer_extras => nameof(webkit_settings_set_enable_developer_extras);
    private static string webkit_settings_set_allow_file_access_from_file_urls => nameof(webkit_settings_set_allow_file_access_from_file_urls);
    private static string webkit_settings_set_allow_modal_dialogs => nameof(webkit_settings_set_allow_modal_dialogs);
    private static string webkit_settings_set_allow_top_navigation_to_data_urls => nameof(webkit_settings_set_allow_top_navigation_to_data_urls);
    private static string webkit_settings_set_allow_universal_access_from_file_urls => nameof(webkit_settings_set_allow_universal_access_from_file_urls);
    private static string webkit_settings_set_enable_back_forward_navigation_gestures => nameof(webkit_settings_set_enable_back_forward_navigation_gestures);
    private static string webkit_settings_set_enable_caret_browsing => nameof(webkit_settings_set_enable_caret_browsing);
    private static string webkit_settings_set_enable_media_capabilities => nameof(webkit_settings_set_enable_media_capabilities);
    private static string webkit_settings_set_enable_media_stream => nameof(webkit_settings_set_enable_media_stream);
    private static string webkit_settings_set_javascript_can_access_clipboard => nameof(webkit_settings_set_javascript_can_access_clipboard);
    private static string webkit_settings_set_javascript_can_open_windows_automatically => nameof(webkit_settings_set_javascript_can_open_windows_automatically);
    private static string webkit_settings_set_enable_fullscreen => nameof(webkit_settings_set_enable_fullscreen);
    private static string webkit_web_context_register_uri_scheme => nameof(webkit_web_context_register_uri_scheme);
    private static string webkit_uri_scheme_request_get_scheme => nameof(webkit_uri_scheme_request_get_scheme);
    private static string webkit_uri_scheme_request_get_uri => nameof(webkit_uri_scheme_request_get_uri);
    private static string webkit_uri_scheme_request_get_path => nameof(webkit_uri_scheme_request_get_path);
    private static string webkit_uri_scheme_request_finish => nameof(webkit_uri_scheme_request_finish);
    private static string webkit_navigation_policy_decision_get_navigation_action => nameof(webkit_navigation_policy_decision_get_navigation_action);
    private static string webkit_navigation_policy_decision_get_request => nameof(webkit_navigation_policy_decision_get_request);
    private static string webkit_navigation_action_get_request => nameof(webkit_navigation_action_get_request);
    private static string webkit_uri_request_get_uri => nameof(webkit_uri_request_get_uri);
    private static string webkit_permission_request_allow => nameof(webkit_permission_request_allow);

    private static readonly gdk_set_allowed_backends_delegate __gdk_set_allowed_backends;
    private static readonly gdk_x11_window_get_xid_delegate __gdk_x11_window_get_xid;
    private static readonly gtk_widget_realize_delegate __gtk_widget_realize_delegate;
    private static readonly g_signal_connect_data_delegate __g_signal_connect_data;
    private static readonly g_memory_input_stream_new_from_data_delegate __g_memory_input_stream_new_from_data;
    private static readonly webkit_user_script_new_delegate __webkit_user_script_new;
    private static readonly webkit_user_script_unref_delegate __webkit_user_script_unref;
    private static readonly webkit_user_content_manager_add_script_delegate __webkit_user_content_manager_add_script;
    private static readonly webkit_user_content_manager_register_script_message_handler_delegate __webkit_user_content_manager_register_script_message_handler;
    private static readonly webkit_javascript_result_get_js_value_delegate? __webkit_javascript_result_get_js_value;
    private static readonly webkit_javascript_result_unref_delegate? __webkit_javascript_result_unref;
    private static readonly jsc_value_is_string_delegate? __jsc_value_is_string;
    private static readonly jsc_value_to_string_delegate? __jsc_value_to_string;
    private static readonly webkit_policy_decision_ignore_delegate __webkit_policy_decision_ignore;
    private static readonly webkit_policy_decision_use_delegate __webkit_policy_decision_use;
    private static readonly webkit_web_view_new_delegate __webkit_web_view_new;
    private static readonly webkit_web_view_get_settings_delegate __webkit_web_view_get_settings;
    private static readonly webkit_web_view_get_user_content_manager_delegate __webkit_web_view_get_user_content_manager;
    private static readonly webkit_web_view_get_context_delegate __webkit_web_view_get_context;
    private static readonly webkit_web_view_load_uri_delegate __webkit_web_view_load_uri;
    private static readonly webkit_web_view_load_html_delegate __webkit_web_view_load_html;
    private static readonly webkit_web_view_go_back_delegate __webkit_web_view_go_back;
    private static readonly webkit_web_view_go_forward_delegate __webkit_web_view_go_forward;
    private static readonly webkit_web_view_can_go_back_delegate __webkit_web_view_can_go_back;
    private static readonly webkit_web_view_can_go_forward_delegate __webkit_web_view_can_go_forward;
    private static readonly webkit_web_view_reload_delegate __webkit_web_view_reload;
    private static readonly webkit_web_view_stop_loading_delegate __webkit_web_view_stop_loading;
    private static readonly webkit_web_view_run_javascript_delegate __webkit_web_view_run_javascript;
    private static readonly webkit_web_view_get_zoom_level_delegate __webkit_web_view_get_zoom_level;
    private static readonly webkit_web_view_set_zoom_level_delegate __webkit_web_view_set_zoom_level;
    private static readonly webkit_settings_set_enable_developer_extras_delegate __webkit_settings_set_enable_developer_extras;
    private static readonly webkit_settings_set_allow_file_access_from_file_urls_delegate __webkit_settings_set_allow_file_access_from_file_urls;
    private static readonly webkit_settings_set_allow_modal_dialogs_delegate __webkit_settings_set_allow_modal_dialogs;
    private static readonly webkit_settings_set_allow_top_navigation_to_data_urls_delegate? __webkit_settings_set_allow_top_navigation_to_data_urls;
    private static readonly webkit_settings_set_allow_universal_access_from_file_urls_delegate __webkit_settings_set_allow_universal_access_from_file_urls;
    private static readonly webkit_settings_set_enable_back_forward_navigation_gestures_delegate __webkit_settings_set_enable_back_forward_navigation_gestures;
    private static readonly webkit_settings_set_enable_caret_browsing_delegate __webkit_settings_set_enable_caret_browsing;
    private static readonly webkit_settings_set_enable_media_capabilities_delegate? __webkit_settings_set_enable_media_capabilities;
    private static readonly webkit_settings_set_enable_media_stream_delegate __webkit_settings_set_enable_media_stream;
    private static readonly webkit_settings_set_javascript_can_access_clipboard_delegate __webkit_settings_set_javascript_can_access_clipboard;
    private static readonly webkit_settings_set_javascript_can_open_windows_automatically_delegate __webkit_settings_set_javascript_can_open_windows_automatically;
    private static readonly webkit_settings_set_enable_fullscreen_delegate __webkit_settings_set_enable_fullscreen;
    private static readonly webkit_web_context_register_uri_scheme_delegate __webkit_web_context_register_uri_scheme;
    private static readonly webkit_uri_scheme_request_get_scheme_delegate __webkit_uri_scheme_request_get_scheme;
    private static readonly webkit_uri_scheme_request_get_uri_delegate __webkit_uri_scheme_request_get_uri;
    private static readonly webkit_uri_scheme_request_get_path_delegate? __webkit_uri_scheme_request_get_path;
    private static readonly webkit_uri_scheme_request_finish_delegate __webkit_uri_scheme_request_finish;
    private static readonly webkit_navigation_policy_decision_get_navigation_action_delegate? __webkit_navigation_policy_decision_get_navigation_action;
    private static readonly webkit_navigation_policy_decision_get_request_delegate? __webkit_navigation_policy_decision_get_request;
    private static readonly webkit_navigation_action_get_request_delegate? __webkit_navigation_action_get_request;
    private static readonly webkit_uri_request_get_uri_delegate __webkit_uri_request_get_uri;
    private static readonly webkit_permission_request_allow_delegate __webkit_permission_request_allow;

    public static bool SetAllowedBackends(string backends)
    {
        if (string.IsNullOrWhiteSpace(backends))
            return false;

        try
        {
           __gdk_set_allowed_backends.Invoke(backends);
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }

    public static nint GetWidgetXid(nint widgetHandle)
    {
        if (widgetHandle == IntPtr.Zero)
            return 0;

        var gdkWindow = Interop_gtk.gtk_widget_get_window(widgetHandle);
        if (gdkWindow == IntPtr.Zero)
            return 0;

        return __gdk_x11_window_get_xid.Invoke(gdkWindow);
    }

    public static void WidgetRealize(nint widgetHandle)
    {
        if (widgetHandle == IntPtr.Zero)
            return;

        __gtk_widget_realize_delegate.Invoke(widgetHandle);
    }

    public static ulong AddSignalConnect(nint instance, string detailed_signal, nint c_handler, nint data)
    {
        return __g_signal_connect_data.Invoke(instance, detailed_signal, c_handler, data, IntPtr.Zero, GConnectFlags.G_CONNECT_AFTER);
    }

    public static nint MarshalToGLibInputStream(byte[] data, uint length, nint destroy) => __g_memory_input_stream_new_from_data.Invoke(data, length, destroy);
    public static nint MarshalToGLibInputStream(byte[] data, long length) => MarshalToGLibInputStream(data, (uint)length, IntPtr.Zero);

    public static nint CreateUserScriptX(string script)
    {
        if (string.IsNullOrWhiteSpace(script))
            return default;

        return __webkit_user_script_new.Invoke(script,
                                                WebKitUserContentInjectedFrames.WEBKIT_USER_CONTENT_INJECT_ALL_FRAMES,
                                                WebKitUserScriptInjectionTime.WEBKIT_USER_SCRIPT_INJECT_AT_DOCUMENT_START,
                                                null, null);
    }

    public static void ReleaseScript(nint scriptHandle) => __webkit_user_script_unref.Invoke(scriptHandle);

    public static void AddScriptForUserContentManager(nint userContentManager, nint script) => __webkit_user_content_manager_add_script.Invoke(userContentManager, script);

    public static bool RegisterScriptMessageHandler(nint userContentManager, string name) => __webkit_user_content_manager_register_script_message_handler.Invoke(userContentManager, name);

    public static nint GetJavaScriptValue(nint jsResult)
    {
        // In webkit2gtk-4.1, the signal passes JSCValue* directly
        // In webkit2gtk-4.0, it passes WebKitJavascriptResult* which needs unwrapping
        if (LinuxApplicationManager.IsWebkit41 || __webkit_javascript_result_get_js_value is null)
            return jsResult;

        return __webkit_javascript_result_get_js_value.Invoke(jsResult);
    }

    public static void ReleaseJavaScriptResult(nint jsResult)
    {
        // In webkit2gtk-4.1, JSCValue is managed by GObject ref counting, no special unref needed
        // In webkit2gtk-4.0, WebKitJavascriptResult needs explicit unref
        if (!LinuxApplicationManager.IsWebkit41 && __webkit_javascript_result_unref is not null)
            __webkit_javascript_result_unref.Invoke(jsResult);
    }

    public static bool IsString(nint value) => __jsc_value_is_string?.Invoke(value) ?? false;
    public static bool IsStringEx(this nint value) => IsString(value);

    public static nint JscToString(nint value) => __jsc_value_to_string?.Invoke(value) ?? IntPtr.Zero;
    public static string ToStringX(nint value)
    {
        var pString = JscToString(value);
        if (pString == IntPtr.Zero)
            return string.Empty;
        var stringValue = Marshal.PtrToStringAuto(pString)!;
        Interop_gtk.g_free(pString);
        return stringValue;
    }
    public static string ToStringEx(this nint value) => ToStringX(value);

    public static void IgnorePolicyDecision(nint decision) => __webkit_policy_decision_ignore.Invoke(decision);
    public static void UsePolicyDecision(nint decision) => __webkit_policy_decision_use.Invoke(decision);

    // WebView functions
    public static nint WebViewNew() => __webkit_web_view_new.Invoke();
    public static nint WebViewGetSettings(nint webView) => __webkit_web_view_get_settings.Invoke(webView);
    public static nint WebViewGetUserContentManager(nint webView) => __webkit_web_view_get_user_content_manager.Invoke(webView);
    public static nint WebViewGetContext(nint webView) => __webkit_web_view_get_context.Invoke(webView);
    public static void WebViewLoadUri(nint webView, string uri) => __webkit_web_view_load_uri.Invoke(webView, uri);
    public static void WebViewLoadHtml(nint webView, string content, string? baseUri = null) => __webkit_web_view_load_html.Invoke(webView, content, baseUri);
    public static void WebViewGoBack(nint webView) => __webkit_web_view_go_back.Invoke(webView);
    public static void WebViewGoForward(nint webView) => __webkit_web_view_go_forward.Invoke(webView);
    public static bool WebViewCanGoBack(nint webView) => __webkit_web_view_can_go_back.Invoke(webView);
    public static bool WebViewCanGoForward(nint webView) => __webkit_web_view_can_go_forward.Invoke(webView);
    public static void WebViewReload(nint webView) => __webkit_web_view_reload.Invoke(webView);
    public static void WebViewStopLoading(nint webView) => __webkit_web_view_stop_loading.Invoke(webView);
    public static void WebViewRunJavascript(nint webView, string script) => __webkit_web_view_run_javascript.Invoke(webView, script, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
    public static double WebViewGetZoomLevel(nint webView) => __webkit_web_view_get_zoom_level.Invoke(webView);
    public static void WebViewSetZoomLevel(nint webView, double zoomLevel) => __webkit_web_view_set_zoom_level.Invoke(webView, zoomLevel);

    // Settings functions
    public static void SettingsSetEnableDeveloperExtras(nint settings, bool enabled) => __webkit_settings_set_enable_developer_extras.Invoke(settings, enabled);
    public static void SettingsSetAllowFileAccessFromFileUrls(nint settings, bool allowed) => __webkit_settings_set_allow_file_access_from_file_urls.Invoke(settings, allowed);
    public static void SettingsSetAllowModalDialogs(nint settings, bool allowed) => __webkit_settings_set_allow_modal_dialogs.Invoke(settings, allowed);
    public static void SettingsSetAllowTopNavigationToDataUrls(nint settings, bool allowed) => __webkit_settings_set_allow_top_navigation_to_data_urls?.Invoke(settings, allowed);
    public static void SettingsSetAllowUniversalAccessFromFileUrls(nint settings, bool allowed) => __webkit_settings_set_allow_universal_access_from_file_urls.Invoke(settings, allowed);
    public static void SettingsSetEnableBackForwardNavigationGestures(nint settings, bool enabled) => __webkit_settings_set_enable_back_forward_navigation_gestures.Invoke(settings, enabled);
    public static void SettingsSetEnableCaretBrowsing(nint settings, bool enabled) => __webkit_settings_set_enable_caret_browsing.Invoke(settings, enabled);
    public static void SettingsSetEnableMediaCapabilities(nint settings, bool enabled) => __webkit_settings_set_enable_media_capabilities?.Invoke(settings, enabled);
    public static void SettingsSetEnableMediaStream(nint settings, bool enabled) => __webkit_settings_set_enable_media_stream.Invoke(settings, enabled);
    public static void SettingsSetJavascriptCanAccessClipboard(nint settings, bool allowed) => __webkit_settings_set_javascript_can_access_clipboard.Invoke(settings, allowed);
    public static void SettingsSetJavascriptCanOpenWindowsAutomatically(nint settings, bool allowed) => __webkit_settings_set_javascript_can_open_windows_automatically.Invoke(settings, allowed);
    public static void SettingsSetEnableFullscreen(nint settings, bool enabled) => __webkit_settings_set_enable_fullscreen.Invoke(settings, enabled);

    // URI scheme functions
    public static void WebContextRegisterUriScheme(nint context, string scheme, nint callback, nint userData) => __webkit_web_context_register_uri_scheme.Invoke(context, scheme, callback, userData, IntPtr.Zero);
    public static string UriSchemeRequestGetScheme(nint request)
    {
        var ptr = __webkit_uri_scheme_request_get_scheme.Invoke(request);
        return Marshal.PtrToStringAnsi(ptr) ?? string.Empty;
    }
    public static string UriSchemeRequestGetUri(nint request)
    {
        var ptr = __webkit_uri_scheme_request_get_uri.Invoke(request);
        return Marshal.PtrToStringAnsi(ptr) ?? string.Empty;
    }
    public static void UriSchemeRequestFinish(nint request, nint stream, long streamLength, string? contentType) => __webkit_uri_scheme_request_finish.Invoke(request, stream, streamLength, contentType);

    // Navigation policy
    public static nint NavigationPolicyDecisionGetRequest(nint decision)
    {
        // Try the direct method first (available in 4.0)
        if (__webkit_navigation_policy_decision_get_request is not null)
            return __webkit_navigation_policy_decision_get_request.Invoke(decision);

        // In newer versions, go through navigation action
        if (__webkit_navigation_policy_decision_get_navigation_action is not null && __webkit_navigation_action_get_request is not null)
        {
            var action = __webkit_navigation_policy_decision_get_navigation_action.Invoke(decision);
            if (action != IntPtr.Zero)
                return __webkit_navigation_action_get_request.Invoke(action);
        }

        return IntPtr.Zero;
    }

    public static string UriRequestGetUri(nint request)
    {
        if (request == IntPtr.Zero)
            return string.Empty;
        var ptr = __webkit_uri_request_get_uri.Invoke(request);
        return Marshal.PtrToStringAnsi(ptr) ?? string.Empty;
    }

    public static void PermissionRequestAllow(nint request) => __webkit_permission_request_allow.Invoke(request);
}
