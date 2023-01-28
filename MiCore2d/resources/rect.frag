#version 330

out vec4 outputColor;

in vec2 texCoord;

uniform float texAlpha;
uniform vec3 color;

void main()
{
  outputColor = mix(vec4(color, 1.0), vec4(0.0, 0.0, 0.0, 0.0), texAlpha);
}
